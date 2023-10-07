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
        [HttpGet]
        public IActionResult Enrollments()
        {
            return View();
        }
    }
}