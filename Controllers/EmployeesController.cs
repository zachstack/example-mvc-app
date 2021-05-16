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
    [Route("api/[controller]")]
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
        public async Task<IEnumerable<Employee>> Get()
        {
            var employees = await database.Employees.ToListAsync();

            return employees;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
