using Microsoft.AspNetCore.Mvc;
using lmsapp.business.Abstract;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Identity;
using lmsapp.webui.Identity;

namespace lmsapp.webui.Controllers
{
    public class StudentController: Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        public StudentController(ICourseService courseService, UserManager<User> userManager)
        {
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
    }
}