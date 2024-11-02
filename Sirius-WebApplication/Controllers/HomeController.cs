using Microsoft.AspNetCore.Mvc;
using Sirius_WebApplication.Data;
using Sirius_WebApplication.Models;
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

       

        public string Index()
        {
            var s = String.Join(", ", _dbContext.Persons.Select(i => $"{i.Id}. {i.FName} {i.LName}"));
            return String.IsNullOrEmpty(s) ? "��� ������ ���" : s;
        }


        public string Add(Person person)
        {
            if (person != null && person.Valid())
            {
                _dbContext.Persons.Add(person);
                _dbContext.SaveChanges();
                return "���������!";
            }
            else return "�� ��� �������� ������������ ����������";
        }

        public string Edit(Person person)
        {
            if (person != null && person.Valid())
            {
                var p = _dbContext.Persons.FirstOrDefault(i => i.Id == person.Id);
                if (p == null)
                    return "������ ������������ �� �� �����";
                var updated = p.Update(person);
                _dbContext.SaveChanges();
                return "���� ��������� ��������� ��������: " + String.Join(",", updated);
            }
            else return "�� ��� �������� ������������ ����������";
        }

        public string Delete(Person person)
        {
            throw new NotImplementedException();
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
