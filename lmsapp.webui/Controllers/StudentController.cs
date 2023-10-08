using Microsoft.AspNetCore.Mvc;
using lmsapp.business.Abstract;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Identity;
using lmsapp.webui.Identity;
using lmsapp.entity;

namespace lmsapp.webui.Controllers
{
    public class StudentController: Controller
    {
        private readonly ICourseService _courseService;
        private readonly IAssignmentService _assignmentService;
        private readonly UserManager<User> _userManager;
        public StudentController(IAssignmentService assignmentService, ICourseService courseService, UserManager<User> userManager)
        {
            _assignmentService = assignmentService;
            _courseService = courseService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Enrollments(){
            var userId = _userManager.GetUserId(User);
            var courses = await _courseService.GetStudentCoursesByUserIdAsync(userId);
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
                Courses = instructorCourses
            });
        }

        public async Task<IActionResult> Detail(int id)
        {
            Course course = await _courseService.GetStudentCourseContentAsync(_userManager.GetUserId(User), id);
            var instructor = await _userManager.FindByIdAsync(course.InstructorId);
            var instructorCourse = new InstructorCourse()
            {
                Course = course,
                Instructor = instructor
            };
            return instructorCourse == null ? NotFound() : View(instructorCourse);
        }
        [HttpPost]
        public async Task<IActionResult> AssignmetSubmit(int[] IdsToSubmit){

            
            return RedirectToAction("Enrollments", "Student");
        }
    }
}