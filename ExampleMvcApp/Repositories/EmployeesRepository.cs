using ExampleMvcApp.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using ExampleMvcApp.Models;

namespace ExampleMvcApp.Repositories
{
    /// <summary>
    /// Interface for an Employees Repository
    /// </summary>
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployees(string name = null, string departmentName = null, string subDepartmentName = null);
        Task<List<Employee>> GetEmployeesDapper(string name = null, string departmentName = null, string subDepartmentName = null);
    }

    /// <summary>
    /// Repository for accessing Database Employees
    /// </summary>
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ExampleDbContext database;
        private readonly IConfiguration config;

        public EmployeesRepository(ExampleDbContext context, IConfiguration configuration)
        {
            database = context;
            config = configuration;
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
            var employees = await database.Employees.FromSqlInterpolated(query)
                .ToListAsync();
                

            return employees;
        }

        public async Task<List<Employee>> GetEmployeesDapper(string name = null, string departmentName = null, string subDepartmentName = null)
        {
            string sql = config.GetConnectionString("ExampleDb");
            using var connection = new SqlConnection(sql);
            await connection.OpenAsync();
            string procedure = "SelectAllEmployees";

            var values = new DynamicParameters();
            values.Add("@Name", name ?? "");
            values.Add("@DepartmentName", departmentName ?? "");
            values.Add("@SubDepartmentName", subDepartmentName ?? "");
            var view = connection.Query<EmployeeAndNames>(procedure, values, commandType: CommandType.StoredProcedure).ToList();

            var results = view.Select(v => new Employee()
            {
                EmployeeId = v.EmployeeId,
                SubDepartmentId = v.SubDepartmentId,
                FirstName = v.FirstName,
                LastName = v.LastName,
                Bio = v.Bio,
                ProfileImage = v.ProfileImage,
                FbprofileLink = v.FbprofileLink,
                TwitterProfileLink = v.TwitterProfileLink,
                AddedDate = v.AddedDate,
                UpdatedDate = v.UpdatedDate,
                Deleted = v.Deleted,
                DeletedDate = v.DeletedDate,
                SubDepartment = new SubDepartment()
                {
                    SubDepartmentName = v.SubDepartmentName,
                    Department = new Department()
                    {
                        DepartmentName = v.DepartmentName
                    }
                }
            }).ToList();

            return results;
        }
    }
}
