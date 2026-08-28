using IdentityMail.web.Context;
using IdentityMail.web.DTOs.UserDtos.UserMessageDtos;
using IdentityMail.web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMail.web.Controllers
{
    [Authorize]
    public class MessageController(UserManager<AppUser> _userManager,
                                   AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(string? filter, int? categoryId, string? sort)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null) return Challenge();

            ViewBag.fullName = $"{user.FirstName} {user.LastName}";
            ViewBag.ProfileImageUrl = user.ProfileImageUrl;

            var query = _context.UserMessages
                .Include(x => x.Sender)
                .Include(x => x.Category)
                .Where(x => x.ReceiverId == user.Id && !x.IsDeleted);

            switch (filter)
            {
                case "read":
                    query = query.Where(x => x.IsRead);
                    break;
                case "unread":
                    query = query.Where(x => !x.IsRead);
                    break;
                case "starred":
                    query = query.Where(x => x.IsStarred);
                    break;
            }

            query = sort switch
            {
                "old" => query.OrderBy(x => x.SendDate),
                "az" => query.OrderBy(x => x.Subject),
                "za" => query.OrderByDescending(x => x.Subject),
                _ => query.OrderByDescending(x => x.SendDate)
            };

            ViewBag.SelectedSort = sort;

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.SelectedCategory = categoryId;

            var messages = await query.ToListAsync();
            return View(messages);
        }

        public async Task<IActionResult> SendMail()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();

            // Reply üzerinden gelen bir mail varsa formda hazır gelsin
            if (TempData["ReceiverMail"] != null)
            {
                var model = new SendMailDto
                {
                    ReceiverMail = TempData["ReceiverMail"]!.ToString()!
                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(SendMailDto sendMailDto)
        {
            var sender = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var receiver = await _userManager.FindByEmailAsync(sendMailDto.ReceiverMail);

            if (receiver == null)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError(string.Empty, "Alıcı bulunamadı.");
                return View(sendMailDto);
            }

            var newMessage = new UserMessage
            {
                SendDate = DateTime.Now,
                ReceiverId = receiver.Id,
                SenderId = sender!.Id,
                Subject = sendMailDto.Subject,
                Body = sendMailDto.Body,
                CategoryId = sendMailDto.CategoryId
            };

            _context.UserMessages.Add(newMessage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MailDetail(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var message = await _context.UserMessages
                .Include(x => x.Sender)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id && (x.ReceiverId == user!.Id || x.SenderId == user.Id));

            if (message == null)
            {
                return NotFound();
            }

            if (message.ReceiverId == user!.Id && !message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStar(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var message = await _context.UserMessages
                .FirstOrDefaultAsync(x => x.Id == id && x.ReceiverId == user!.Id);

            if (message == null)
            {
                return NotFound();
            }

            message.IsStarred = !message.IsStarred;
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> StarredMessages()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            ViewBag.fullName = $"{user!.FirstName} {user.LastName}";
            ViewBag.ProfileImageUrl = user.ProfileImageUrl;

            var messages = await _context.UserMessages
                .Include(x => x.Sender)
                .Where(x => x.ReceiverId == user.Id && x.IsStarred && !x.IsDeleted)
                .OrderByDescending(x => x.SendDate)
                .ToListAsync();

            return View(messages);
        }

        public async Task<IActionResult> SentMessages()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            ViewBag.fullName = $"{user!.FirstName} {user.LastName}";
            ViewBag.ProfileImageUrl = user.ProfileImageUrl;

            var messages = await _context.UserMessages
                .Include(x => x.Receiver)
                .Where(x => x.SenderId == user.Id && !x.IsDeleted)
                .OrderByDescending(x => x.SendDate)
                .ToListAsync();

            return View(messages);
        }

        public async Task<IActionResult> Reply(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var message = await _context.UserMessages
                .Include(x => x.Sender)
                .FirstOrDefaultAsync(x => x.Id == id && x.ReceiverId == user!.Id);

            if (message == null)
            {
                return NotFound();
            }

            TempData["ReceiverMail"] = message.Sender.Email;
            return RedirectToAction(nameof(SendMail));
        }

        public async Task<IActionResult> MoveToTrash(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var message = await _context.UserMessages
                .FirstOrDefaultAsync(x => x.Id == id && x.ReceiverId == user!.Id);

            if (message == null)
            {
                return NotFound();
            }

            message.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Trash()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            ViewBag.fullName = $"{user!.FirstName} {user.LastName}";
            ViewBag.ProfileImageUrl = user.ProfileImageUrl;

            var messages = await _context.UserMessages
                .Include(x => x.Sender)
                .Where(x => x.ReceiverId == user.Id && x.IsDeleted)
                .OrderByDescending(x => x.SendDate)
                .ToListAsync();

            return View(messages);
        }

        public async Task<IActionResult> Restore(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var message = await _context.UserMessages
                .FirstOrDefaultAsync(x => x.Id == id && x.ReceiverId == user!.Id);

            if (message == null)
            {
                return NotFound();
            }

            message.IsDeleted = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Trash));
        }
    }
}