using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using lmsapp.webui.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lmsapp.webui.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        private readonly IAssignmentService _assignmentService;
        private readonly IAssigneeService _assigneeService;
        private readonly ISectionService _sectionService;
        private readonly IContentService _contentService;
        public InstructorController(IAssignmentService assignmentService, IAssigneeService assigneeService, ISectionService sectionService, IContentService contentService, ICourseService courseService, UserManager<User> userManager)
        {
            _assignmentService = assignmentService;
            _assigneeService = assigneeService;
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

        public async Task<IActionResult> CourseEdit(int id)
        {
            Course course = await _courseService.GetInstructorCourseContentAsync(id);
            return course == null ? NotFound() : View(course);
        }

        [HttpPost]
        public async Task<IActionResult> CourseEdit(Course model)
        {
            if (ModelState.IsValid)
            {
                /*await _courseService.UpdateAsync(new Course
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
                });*/
                model.InstructorId = _userManager.GetUserId(User);
                model.isUpdated = true;
                await _courseService.UpdateAsync(model);
                return RedirectToAction("Lectures", "Instructor");
            }
            return View(model);
        }
        public IActionResult CourseCreate() => View();
        public async Task<IActionResult> Assignments()
        {
            var courses = await _courseService.GetInstructorCoursesByUserIdAsync(_userManager.GetUserId(User));
            var assignments = new List<AssigneeModel>();
            foreach (var c in courses)
            {
                var courseAssignments = await _assignmentService.GetAssignmentsByCourseIdAsync(c.Id);
                foreach (var a in courseAssignments)
                {
                    foreach (var i in a.Assignees)
                    {
                        var user = await _userManager.FindByIdAsync(i.UserId);
                        var userName = user.UserName;
                        assignments.Add(new AssigneeModel
                        {
                            Id = a.AssignmentId,
                            Username = userName,
                            Title = a.Title,
                            CourseTitle = c.Title,
                            Description = a.Description,
                            DueDate = a.DueDate,
                            IsSubmitted = i.isSubmitted
                        });
                    }
                }
            }
            return View(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> CourseCreate(Course model)
        {
            if (ModelState.IsValid)
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
                return RedirectToAction("Lectures", "Instructor");
            }
            return View(model);
        }

        public async Task<IActionResult> AssignmentCreate()
        {
            var courses = await _courseService.GetInstructorCoursesByUserIdAsync(_userManager.GetUserId(User));
            var selectList = new List<SelectListItem>();
            foreach (var item in courses)
            {
                selectList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            ViewBag.Courses = selectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentCreate(Assignment model)
        {
            if (ModelState.IsValid)
            {
                await _assignmentService.CreateAsync(new Assignment
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    CourseId = model.CourseId,
                });
                var lastIds = await _assignmentService.GetAllAsync();
                var users = await _userManager.GetUsersInRoleAsync("Student");
                var students = new List<Assignee>();
                var course = await _courseService.GetCourseByIdAsync(model.CourseId);
                foreach (var user in users)
                {
                    var studentCourse = await _courseService.GetStudentCoursesByUserIdAsync(user.Id);
                    if (studentCourse.Contains(course))
                    {
                        students.Add(new Assignee
                        {
                            UserId = user.Id,
                            isSubmitted = false
                        });
                    }
                }
                foreach (var student in students)
                {
                    await _assigneeService.CreateAsync(new Assignee
                    {
                        UserId = student.UserId,
                        AssignmentId =  lastIds.LastOrDefault().AssignmentId,
                        isSubmitted = student.isSubmitted
                    });
                }

                return RedirectToAction("Lectures", "Instructor");
            }
            return View("CourseCreate", model);
        }
        [HttpPost]
        public async Task<IActionResult> SectionCreate(string[] sending)
        {
            await _sectionService.CreateAsync(new Section
            {
                CourseId = int.Parse(sending[0]),
                Title = sending[1],
            });
            return RedirectToAction("Detail", "Instructor", new { id = int.Parse(sending[0]) });
        }

        public async Task<IActionResult> ContentCreate(int id){
            var course = await _courseService.GetInstructorCourseContentAsync(id);
            var selectList = new List<SelectListItem>();
            foreach (var item in course.Sections)
            {
                selectList.Add(new SelectListItem { Value = item.SectionId.ToString(), Text = item.Title });
            }
            ViewBag.Sections = selectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContentCreate(Content model)
        {
            if (ModelState.IsValid)
            {
                await _contentService.CreateAsync(new Content
                {
                    Title = model.Title,
                    SectionId = model.SectionId,
                    VideoUrl = model.VideoUrl,
                });
                return RedirectToAction("ContentCreate", "Instructor", new { id = model.SectionId });
            }
            return View("CourseCreate", model);
        }
    }
}