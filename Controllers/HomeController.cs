using ExampleMvcApp.Models;
using ExampleMvcApp.Models.ViewModels;
using ExampleMvcApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeesRepository _repository;

        public HomeController(ILogger<HomeController> logger, IEmployeesRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Employees()
        {
            var allEmployees = await _repository.GetEmployees();
            var model = new EmployeeViewModel(allEmployees);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
