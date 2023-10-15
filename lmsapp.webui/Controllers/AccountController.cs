using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using lmsapp.business.Abstract;
using lmsapp.webui.EmailServices;
using lmsapp.webui.Extensions;
using lmsapp.webui.Models;
using lmsapp.webui.Identity;

namespace lmsapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Login(string ReturnUrl = null) => View(new LoginModel() { ReturnUrl = ReturnUrl });
        public IActionResult Register() => View();
        public IActionResult ForgotPassword() => View();
        public IActionResult AccessDenied() => View();

        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var selectedRoles = await _userManager.GetRolesAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
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

        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(UserDetailsModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray());
                        if (selectedRoles.Contains("Student"))
                        {
                            foreach (var item in userRoles)
                            {
                                if (item != "Student")
                                {
                                    await _userManager.RemoveFromRoleAsync(user, item);
                                }
                            }
                        }
                        else
                        {
                            await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray());
                        }
                        return RedirectToAction("List", "Course");
                    }
                }
                return RedirectToAction("Profile");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "No account has been created with this username before.");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please confirm your membership with the link sent to your email account.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("List", "Course");
            }
            ModelState.AddModelError("", "The username or password entered is incorrect.");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Student");

            if (result.Succeeded)
            {
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                await _emailSender.SendEmailAsync(model.Email, "Confirm Email.", $"Please click on the <a href='http://localhost:5278{url}'>link</a> to confirm your email account.");
                var msg = new AlertMessage()
                {
                    Message = "Please confirm your email account",
                    AlertType = "warning"
                };
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "An unknown error occurred.");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View();
            }
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return View();
            }
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            await _emailSender.SendEmailAsync(Email, "Reset Password", $"Parolanızı yenilemek için linke <a href='http://localhost:5278{url}'>tıklayınız.</a>");
            return View();
        }
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Course", "List");
            }
            _ = new ResetPasswordModel { Token = token };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Course", "List");
            }
            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            return result.Succeeded ? RedirectToAction("Login", "Account") : View(model);
        }
    }
}