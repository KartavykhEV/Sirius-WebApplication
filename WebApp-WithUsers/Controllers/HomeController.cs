using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp_WithUsers.Data;
using WebApp_WithUsers.Models;

namespace WebApp_WithUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] ApplicationDbContext db,
            [FromServices] UserManager<IdentityUser> manager)
        {
            var id = manager.GetUserId(User);
            var dbUser = db.Users.FirstOrDefault(u => u.Id == id);
            var userName = dbUser.UserName;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
