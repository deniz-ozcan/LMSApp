using Microsoft.AspNetCore.Mvc;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Identity;
using lmsapp.webui.Identity;
using Newtonsoft.Json;

namespace lmsapp.webui.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly UserManager<User> _userManager;
        public CourseController(ICourseService courseService, IEnrollmentService enrollmentService, UserManager<User> userManager)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }
        public IActionResult LandingPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Course");
            }
            return View();
        }
        public async Task<IActionResult> Index(string q, int page = 1)
        {
            var courses = await _courseService.GetCoursesAsync(q, page, 8);
            var instructorCourses = new List<InstructorCourse>();
            foreach (var course in courses)
            {
                var instructor = await _userManager.FindByIdAsync(course.InstructorId);
                instructorCourses.Add(new InstructorCourse()
                {
                    Course = course,
                    Instructor = instructor
                });
            }
            return View(new CourseViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = await _courseService.GetCoursesCountAsync(q),
                    CurrentPage = page,
                    ItemsPerPage = 8,
                    CurrentCategory = q
                },
                Courses = instructorCourses
            });
        }
        public async Task<IActionResult> Detail(int id)
        {
            Course course = await _courseService.GetCourseByIdAsync(id);
            var instructor = await _userManager.FindByIdAsync(course.InstructorId);
            var instructorCourse = new InstructorCourse()
            {
                Course = course,
                Instructor = instructor
            };
            return instructorCourse == null ? NotFound() : View(instructorCourse);
        }
        [HttpPost]
        public async Task<IActionResult> Enroll(int courseId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Instructor"))
            {
                Course course = await _courseService.GetCourseByIdAsync(courseId);
                if (course.InstructorId == user.Id)
                {
                    TempData["message"] = JsonConvert.SerializeObject(new AlertMessage() { Message = "You cannot enroll in your own course.", AlertType = "danger" });
                    return RedirectToAction("Detail", "Course", new { id = courseId });
                }
            }
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                if (!_enrollmentService.isEnrolled(courseId, userId))
                {
                    await _enrollmentService.CreateAsync(new Enrollment() { CourseId = courseId, UserId = userId });
                    Course crs = await _courseService.GetCourseByIdAsync(courseId);
                    crs.EnrollmentCount++;
                    await _courseService.UpdateAsync(crs);
                }
                return RedirectToAction("Enrollments", "Student");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}