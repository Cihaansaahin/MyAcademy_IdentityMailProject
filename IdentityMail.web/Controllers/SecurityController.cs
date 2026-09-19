using IdentityMail.web.DTOs.ProfileDtos;
using IdentityMail.web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace IdentityMail.Web.Controllers
{
    [Authorize]
    public class SecurityController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SecurityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name);

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword);

            if (result.Succeeded)
            {
                TempData["Success"] = "Şifreniz başarıyla değiştirildi.";

                return RedirectToAction("ChangePassword");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresine sahip bir kullanıcı bulunamadı.");
                return View(model);
            }

            TempData["Email"] = model.Email;

            return RedirectToAction(nameof(ChangePassword));
        }
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordDto
            {
                Email = TempData["Email"]?.ToString()
            });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);


            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }


            var removeResult = await _userManager.RemovePasswordAsync(user);

            if (!removeResult.Succeeded)
            {
                foreach (var error in removeResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }


            var addResult = await _userManager.AddPasswordAsync(user, model.NewPassword);


            if (addResult.Succeeded)
            {
                TempData["Success"] = "Şifreniz başarıyla yenilendi.";

                return RedirectToAction("Login", "Auth");
            }


            foreach (var error in addResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }

}
