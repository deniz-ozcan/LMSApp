using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.webui.Identity;
using lmsapp.webui.Models;

namespace lmsapp.webui.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdminController(ICourseService courseService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _courseService = courseService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserDetailsModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                        return RedirectToAction("AdminPanel");
                    }
                }   
                return RedirectToAction("AdminPanel");
            }
            return View(model);
        }
        public IActionResult UserList() => View(_userManager.Users);
        public IActionResult RoleList() => View(_roleManager.Roles);
        public async Task<IActionResult> AdminPanel(){
            // return View(new AdminPanelModel()
            // {
            //     Users = _userManager.Users,
            //     Roles =  _roleManager.Roles,
            //     Courses = await _courseService.GetAllCoursesAsync()
            // });
            var courses = await _courseService.GetAllAsync();
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
            return View(new AdminPanelModel()
            {
                Users = _userManager.Users,
                Roles =  _roleManager.Roles,
                Courses = instructorCourses
            });
        }
        public IActionResult RoleCreate() => View();
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonmembers = new List<User>();
            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                return RedirectToAction("AdminPanel");
            }
            return Redirect("/Admin/Role/" + model.RoleId);
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var entity = await _courseService.GetCourseByIdAsync(id);

            if (entity != null)
            {
                _courseService.Delete(entity);
            }

            var msg = new AlertMessage()
            {
                Message = $"{entity.Title} isimli Kurs silindi.",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("AdminPanel");
        }

        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            var msg = new AlertMessage()
            {
                Message = $"{role.Name} isimli rol silindi.",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("AdminPanel");
        }

        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            var msg = new AlertMessage()
            {
                Message = $"{user.UserName} isimli kullanıcı silindi.",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("AdminPanel");
        }
    }
}