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
using System.Diagnostics;

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
        private readonly IConfiguration config;

        public EmployeesRepository(IConfiguration configuration)
        {
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
            using var connection = new SqlConnection(config.GetConnectionString("ExampleDb"));
            try
            {
                await connection.OpenAsync();

                //Define Procedure and parameters
                string procedure = "SelectAllEmployees";
                var values = new DynamicParameters();
                values.Add("@Name", name?.Trim() ?? "");
                values.Add("@DepartmentName", departmentName?.Trim() ?? "");
                values.Add("@SubDepartmentName", subDepartmentName?.Trim() ?? "");

                //Execute Procedure
                var view = connection.Query<EmployeeAndNames>(procedure, values, commandType: CommandType.StoredProcedure).ToList();

                return view.Select(v => new Employee(v)).ToList();
            }
            catch(Exception ex)
            {
                Trace.TraceError("An error occured while attempting to GetEmployees");
                Trace.TraceError($"Exception Message: {ex.Message}");
                Trace.TraceError($"Exception StackTrace: {ex.StackTrace}");
            }
            finally
            {
                await connection.CloseAsync();
            }

            return new List<Employee>();
        }
    }
}
