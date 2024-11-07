using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sirius_WebApplicationUsers.Data;
using Sirius_WebApplicationUsers.Models;
using System.Diagnostics;

namespace Sirius_WebApplicationUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region управление ролями

        [Authorize(Roles = "Administrator")]
        public IActionResult Roles([FromServices] ApplicationDbContext db)
        {
            var roles = db.Roles.ToArray();
            return View(roles);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddRole([FromServices] ApplicationDbContext db, string name)
        {
            if (!db.Roles.Any(x => x.Name.ToLower() == name.ToLower()))
            {
                db.Roles.Add(new IdentityRole(name) { NormalizedName = name.ToUpper() });
                db.SaveChanges();
            }
            return RedirectToAction("Roles");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddUserToRole([FromServices] UserManager<PortalUser> manager,
            [FromServices] ApplicationDbContext dbContext,
            string userName, string roleName)
        {
            var user = dbContext.Users.FirstOrDefault(i => i.UserName.ToLower() == userName.ToLower());
            if (user != null)
            {
                await manager.AddToRoleAsync(user, roleName);
            }
            return RedirectToAction("Roles");
        }

        #endregion


        [Authorize]
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
