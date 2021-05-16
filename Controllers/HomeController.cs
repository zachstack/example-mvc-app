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

        /// <summary>
        /// Returns the view for the Home page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the view for the Privacy Policy page.
        /// </summary>
        /// <remarks>
        /// Route is /privacy
        /// </remarks>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Returns the view for the Employeess page. Allows for optional search parameters.
        /// </summary>
        /// <remarks>
        /// Route is /employees
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="departmentName"></param>
        /// <param name="subDepartmentName"></param>
        /// <returns></returns>
        public async Task<IActionResult> Employees(string name, string departmentName, string subDepartmentName)
        {
            var allEmployees = await _repository.GetEmployees(name, departmentName, subDepartmentName);
            var model = new EmployeeViewModel(allEmployees, name, departmentName, subDepartmentName);
            return View(model);
        }

        /// <summary>
        /// Returns the view for the Error page.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
