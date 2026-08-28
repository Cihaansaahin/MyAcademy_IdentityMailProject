using IdentityMail.web.DTOs.ProfileDtos;
using IdentityMail.web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace IdentityMail.web.Controllers
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

    }
    
}
