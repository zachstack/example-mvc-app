using ExampleMvcApp.Models.Database;
using ExampleMvcApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleMvcApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _repository;

        public EmployeesController(IEmployeesRepository rep)
        {
            _repository = rep;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees(
            [FromQuery]string name, 
            [FromQuery(Name = "department_name")] string departmentName,
            [FromQuery(Name = "sub_department_name")] string subDepartmentName)
        {
            //Get the employees
            var employees = await _repository.GetEmployees(name, departmentName, subDepartmentName);

            return Ok(employees);
        }
    }
}
