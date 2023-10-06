using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;

namespace lmsapp.webui.Controllers
{
    public class StudentController: Controller
    {
        private readonly ICourseService _courseService;
        public StudentController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public IActionResult Enroll(string userId, int courseId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetAssignments()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitAssignment()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrolledCourses(string userId)
        {
            var courses = await _courseService.GetStudentCoursesByUserIdAsync(userId);
            return View(courses);
        }
        // [HttpGet]
        // public async Task<IActionResult> GetCourseAssignments(int courseId)
        // {
        //     // var assignments = await _courseService.GetCourseAssignmentsAsync(courseId);
        //     return View(assignments);
        // }
        [HttpGet]
        public async Task<IActionResult> GetCourse(string userId, int courseId)
        {
            var assignments = await _courseService.GetStudentCoursesByUserIdAsync(userId);
            return View(assignments);
        }
    }
}