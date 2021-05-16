using ExampleMvcApp.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMvcApp.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployees(string name, string departmentName, string subDepartmentName);
    }

    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ExampleDbContext database;
        public EmployeesRepository(ExampleDbContext context)
        {
            database = context;
        }

        public async Task<List<Employee>> GetEmployees(string name, string departmentName, string subDepartmentName)
        {
            //Trim and format string and convert to empty string in cases of null
            name = name?.Trim() ?? "";
            departmentName = departmentName?.Trim() ?? "";
            subDepartmentName = subDepartmentName?.Trim() ?? "";

            //Sql query to execute
            FormattableString query = $"SelectAllEmployees {name}, {departmentName}, {subDepartmentName}";

            //Get the employees
            var employees = await database.Employees.FromSqlInterpolated(query).ToListAsync();

            return employees;
        }

    }
}
