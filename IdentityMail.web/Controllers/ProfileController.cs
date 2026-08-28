using Microsoft.EntityFrameworkCore;
using IdentityMail.web.Context;
using IdentityMail.web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMail.web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<AppUser> userManager,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.InboxCount = await _context.UserMessages
                .CountAsync(x => x.ReceiverId == user.Id && !x.IsDeleted);

            ViewBag.SentCount = await _context.UserMessages
                .CountAsync(x => x.SenderId == user.Id && !x.IsDeleted);

            ViewBag.StarredCount = await _context.UserMessages
                .CountAsync(x => x.ReceiverId == user.Id && x.IsStarred && !x.IsDeleted);

            ViewBag.UnreadCount = await _context.UserMessages
                .CountAsync(x => x.ReceiverId == user.Id && !x.IsRead && !x.IsDeleted);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(AppUser model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name);

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.ProfileImageUrl = model.ProfileImageUrl;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Profil bilgileriniz güncellendi.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index", user);
        }
    }
}
