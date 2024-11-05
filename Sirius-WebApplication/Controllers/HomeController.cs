using Microsoft.AspNetCore.Mvc;
using Sirius_WebApplication.Data;
using Sirius_WebApplication.Models;
using System;
using System.Diagnostics;

namespace Sirius_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }



        public IActionResult Index()
        {
            var items = _dbContext.Persons.ToArray();
            ViewData["myItem"] = "myValue";


            return View(items);  //String.IsNullOrEmpty(s) ? "еще никого нет" : s;
        }

        public IActionResult PersonDetails(int id)
        {
            var person = _dbContext.Persons.FirstOrDefault(x => x.Id == id); 
            if(person == null) return NotFound();
            return View(person);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SavePerson(Person person)
        {
            if (person != null && person.Valid())
            {
                var p = _dbContext.Persons.FirstOrDefault(i => i.Id == person.Id);
                if (p == null)
                    return NotFound("Такого пользователя мы не нашли");
                var updated = p.Update(person);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else return NotFound("Не все атрибуты определены");

        }


        public IActionResult Add([FromServices] ApplicationDbContext db,
            Person person)
        {
            if (person != null && person.Valid())
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return NotFound("Не все поля определены");
        }

        public string Edit(Person person)
        {
            if (person != null && person.Valid())
            {
                var p = _dbContext.Persons.FirstOrDefault(i => i.Id == person.Id);
                if (p == null)
                    return "Такого пользователя мы не нашли";
                var updated = p.Update(person);
                _dbContext.SaveChanges();
                return "Были обновлены следующие атрибуты: " + String.Join(",", updated);
            }
            else return "Не все атрибуты пользователя определены";
        }

        public string Delete(int id)
        {
            var p = _dbContext.Persons.FirstOrDefault(i => i.Id == id);
            if (p == null)
                return "Такого пользователя мы не нашли";
            _dbContext.Persons.Remove(p);
            _dbContext.SaveChanges();
            return $"Пользователь {p.FName} {p.LName} удален.";
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
