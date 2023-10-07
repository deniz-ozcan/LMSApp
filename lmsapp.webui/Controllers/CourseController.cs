using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Identity;

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
        public async Task<IActionResult> Index(string q, int page = 1)
        {
            return View(new CourseViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = await _courseService.GetCoursesCountAsync(q),
                    CurrentPage = page,
                    ItemsPerPage = 8,
                    CurrentCategory = q
                },
                Courses = await _courseService.GetCoursesAsync(q, page, 8)
            });
        }
        public async Task<IActionResult> Detail(int id)
        {
            Course course = await _courseService.GetCourseByIdAsync(id);
            return course == null ? NotFound() : View(course);
        }
        [HttpPost]
        public IActionResult Enroll(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            _enrollmentService.Enroll(id, userId);
            return RedirectToAction("Detail", new { id });
        }
    }
}