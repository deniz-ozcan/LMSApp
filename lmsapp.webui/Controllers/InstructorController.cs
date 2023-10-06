using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Authorization;

namespace lmsapp.webui.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController  : Controller
    {
        private ICourseService _courseService;
        public InstructorController(ICourseService courseService)
        {
            _courseService = courseService;
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
            if (id < 1)
            {
                return await Task.FromResult<IActionResult>(BadRequest());
            }
            Course course = await _courseService.GetCourseByIdAsync(id);
            return course == null ? NotFound() : View(course);
        }
        public async Task<IActionResult> RestApi()
        {
            var courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync("http://localhost:4200/api/courses");
                string apiResponse = await response.Content.ReadAsStringAsync();
                courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
            }
            return View(courses);
        }
    }
}