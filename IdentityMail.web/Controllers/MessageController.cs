using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMail.web.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
