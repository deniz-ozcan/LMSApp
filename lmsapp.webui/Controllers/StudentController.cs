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
        private readonly IAssigneeService _assigneeService;
        private readonly UserManager<User> _userManager;
        public StudentController(IAssignmentService assignmentService, ICourseService courseService, UserManager<User> userManager, IAssigneeService assigneeService)
        {
            _assignmentService = assignmentService;
            _assigneeService = assigneeService;
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
        public async Task<IActionResult> AssignmetSubmit(int[] submitedAssn, int[] assignmentIds){
            for (int i = 0; i < submitedAssn.Count(); i++)
            {
                await _assigneeService.UpdateAsync(
                    new Assignee{
                        AssigneeId = submitedAssn[i],
                        UserId = _userManager.GetUserId(User),
                        AssignmentId = assignmentIds[i],
                        isSubmitted = true
                    }
                );
            }
            return RedirectToAction("Assignments", "Student");
        }

        public async Task<IActionResult> Assignments(){
            var userId = _userManager.GetUserId(User);
            var assignments = await _assignmentService.GetAssignmentsByUserIdAsync(userId);
            var studentAssignments = new List<AssigntmentModel>();
            foreach (var assignment in assignments)
            {
                foreach(var assignee in assignment.Assignees){
                    studentAssignments.Add(new AssigntmentModel{
                        Assignment = assignment,
                        IsSubmitted = assignee.isSubmitted,
                        AssigneeId = assignee.AssigneeId
                    });
                }
            }
            return View(studentAssignments);
        }


    }
}