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
    [Authorize(Roles = "Instructor")]
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
        public IActionResult Lectures() => View();
        public IActionResult CourseCreate() => View();

        public async Task<IActionResult> CourseCreate(Course model)
        {
            var userId = _userManager.GetUserId(User);
            if(ModelState.IsValid)
            {
                await _courseService.CreateAsync(new Course
                {
                    InstructorId = userId,
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
        public async Task<IActionResult> TaskCreate(Assignment model)
        {
            if(ModelState.IsValid)
            {
                await _assignmentService.CreateAsync(new Assignment
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    CourseId = model.CourseId,
                    IsSubmitted = false
                });
                return RedirectToAction("Lectures","Instructor");
            }
            return View("CourseCreate",model);
        }
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