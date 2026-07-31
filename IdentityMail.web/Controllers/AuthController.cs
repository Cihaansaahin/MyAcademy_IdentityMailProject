using IdentityMail.web.DTOs.UserDtos;
using IdentityMail.web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityMail.web.Controllers
{
    public class AuthController(UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager) : Controller
    {
       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(registerDto.Password != registerDto.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Şifreler Birbiriyle Uyumlu Değil.");
                return View(registerDto);
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(registerDto);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user== null)
            {

                ModelState.AddModelError(string.Empty, "Bu Email Sistemde Kayıtlı Değil.");
                return View(loginDto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email veya Şifre hatalı");
                return View(loginDto);
            }



            return RedirectToAction("Index","Message");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
