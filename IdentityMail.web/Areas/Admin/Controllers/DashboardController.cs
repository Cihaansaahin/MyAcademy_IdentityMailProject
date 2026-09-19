using IdentityMail.web.Areas.Admin.Models;
using IdentityMail.web.Context;
using IdentityMail.web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMail.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(
            AppDbContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalUsers = await _userManager.Users.CountAsync(),

                TotalMessages = await _context.UserMessages.CountAsync(),

                TodayMessages = await _context.UserMessages
                    .CountAsync(x => x.SendDate.Date == DateTime.Today),

                UnreadMessages = await _context.UserMessages
                    .CountAsync(x => !x.IsRead),

                TrashMessages = await _context.UserMessages
                    .CountAsync(x => x.IsDeleted)
            };

            return View(model);
        }
    }
}