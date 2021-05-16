using ExampleMvcApp.Models.Database;
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
        private readonly ExampleDbContext database;

        public EmployeesController(ExampleDbContext context)
        {
            database = context;
        }

        // GET: api/employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees(
            [FromQuery]string name, 
            [FromQuery(Name = "department_name")] string departmentName,
            [FromQuery(Name = "sub_department_name")] string subDepartmentName)
        {
            string query = $"SelectAllEmployees '{name?.Trim()}', '{departmentName?.Trim()}', '{subDepartmentName?.Trim()}'";
            var employees = database.Employees.FromSqlRaw(query);

            return employees;
        }
    }
}
