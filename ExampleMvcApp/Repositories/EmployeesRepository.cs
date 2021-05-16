using ExampleMvcApp.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMvcApp.Repositories
{
    /// <summary>
    /// Interface for an Employees Repository
    /// </summary>
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployees(string name = null, string departmentName = null, string subDepartmentName = null);
    }

    /// <summary>
    /// Repository for accessing Database Employees
    /// </summary>
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ExampleDbContext database;

        public EmployeesRepository(ExampleDbContext context)
        {
            database = context;
        }

        /// <summary>
        /// Gets a list of <see cref="Employee"/>s from the database with optional search parameters
        /// </summary>
        /// <remarks>
        /// Utilizes a Stored Procedure
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="departmentName"></param>
        /// <param name="subDepartmentName"></param>
        /// <returns>A list of <see cref="Employee"/>s</returns>
        public async Task<List<Employee>> GetEmployees(string name = null, string departmentName = null, string subDepartmentName = null)
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
