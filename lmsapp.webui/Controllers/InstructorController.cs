using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using lmsapp.webui.Identity;

namespace lmsapp.webui.Controllers
{
    public class InstructorController  : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        private readonly IAssignmentService _assignmentService;
        private readonly ISectionService _sectionService;
        private readonly IContentService _contentService;
        public InstructorController(IAssignmentService assignmentService, ISectionService sectionService, IContentService contentService, ICourseService courseService, UserManager<User> userManager)
        {
            _assignmentService = assignmentService;
            _sectionService = sectionService;
            _contentService = contentService;
            _courseService = courseService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Lectures()
        {
            var userId = _userManager.GetUserId(User);
            var course = await _courseService.GetInstructorCoursesByUserIdAsync(userId);
            return View(course);
        }


        public async Task<IActionResult> Detail(int id)
        {
            Course course = await _courseService.GetInstructorCourseContentAsync(id);
            return course == null ? NotFound() : View(course);
        }


        public IActionResult CourseCreate() => View();

        [HttpPost]
        public async Task<IActionResult> CourseCreate(Course model)
        {
            if(ModelState.IsValid)
            {
                await _courseService.CreateAsync(new Course
                {
                    InstructorId = _userManager.GetUserId(User),
                    Title = model.Title,
                    Description = model.Description,
                    Rate = model.Rate,
                    TotalHours = model.TotalHours,
                    Level = model.Level,
                    Category = model.Category,
                    ImageUrl = model.ImageUrl,
                    isUpdated = true
                });
                return RedirectToAction("Lectures","Instructor");
            }
            return View(model);
        }
        // public async Task<IActionResult> TaskCreate(Assignment model)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         await _assignmentService.CreateAsync(new Assignment
        //         {
        //             Title = model.Title,
        //             Description = model.Description,
        //             DueDate = model.DueDate,
        //             CourseId = model.CourseId,
        //         });
        //         return RedirectToAction("Lectures","Instructor");
        //     }
        //     return View("CourseCreate",model);
        // }
        public async Task<IActionResult> SectionCreate(Section model)
        {
            if(ModelState.IsValid)
            {
                await _sectionService.CreateAsync(new Section
                {
                    Title = model.Title,
                    CourseId = model.CourseId
                });
                return RedirectToAction("Lectures","Instructor");
            }
            return View("CourseCreate",model);
        }
        public async Task<IActionResult> ContentCreate(Content model)
        {
            if(ModelState.IsValid)
            {
                await _contentService.CreateAsync(new Content
                {
                    Title = model.Title,
                    SectionId = model.SectionId,
                    VideoUrl = model.VideoUrl,
                });
                return RedirectToAction("Lectures","Instructor");
            }
            return View("CourseCreate",model);
        }
    }
}