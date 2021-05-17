using ExampleMvcApp.Controllers;
using ExampleMvcApp.Models.Database;
using ExampleMvcApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExampleMvcApp.Tests.TestCases
{

    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeesController _controller;

        [SetUp]
        public void Setup()
        {
            //Get Config file
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Development.json")
                    .Build();

            //Setup the necessary services
            var services = new ServiceCollection();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddSingleton<IConfiguration>(config);
            var serviceProvider = services.BuildServiceProvider();

            //Get the needed repository
            var repository = serviceProvider.GetService<IEmployeesRepository>();

            _controller = new EmployeesController(repository);
        }

        [Test]
        public async Task GetEmployees_WithNoParameters_ShouldBeOk()
        {
            //Execute
            var response = await _controller.GetEmployees(null, null, null);

            //Assert
            Assert.NotNull(response);
            Assert.IsTrue(response is OkObjectResult);

            var result = response as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Value is List<Employee>);

            var body = (List<Employee>)result.Value;
            Assert.IsNotEmpty(body);
            Assert.IsTrue(body.Count == body.Select(e => e.EmployeeId).Count());
        }

        /// <summary>
        /// Tests the endpoint GetEmployees with a variety of combinations of search parameters.
        /// </summary>
        /// <remarks>
        /// Test cases assumes a static database and that there are no duplicate names.
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="departmentName"></param>
        /// <param name="subDepartmentName"></param>
        /// <returns></returns>
        [Test]
        [TestCase("Andrea Arkov", null, null, ExpectedResult = 4)]
        [TestCase("Andrea", null, null, ExpectedResult = 4)]
        [TestCase("Andrea Arkov", "Design Department", null, ExpectedResult = 4)]
        [TestCase("Andrea Arkov", null, "Web Designer", ExpectedResult = 4)]
        [TestCase("Andrea Arkov", "Design Department", "Web Designer", ExpectedResult = 4)]
        [TestCase(null, null, "Web Designer", ExpectedResult = 4)]
        [TestCase(null, null, "Web", ExpectedResult = 4)]
        [TestCase(null, "Design Department", "Web Designer", ExpectedResult = 4)]
        public async Task<int> GetEmployees_WithSearchParameters_ShouldBeOneEmployee(string name, string departmentName, string subDepartmentName)
        {
            //Execute
            var response = await _controller.GetEmployees(name, departmentName, subDepartmentName);

            //Assert
            Assert.NotNull(response);
            Assert.IsTrue(response is OkObjectResult);

            var result = response as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Value is List<Employee>);

            var body = (List<Employee>)result.Value;
            Assert.AreEqual(1, body.Count);

            return body[0].EmployeeId;
        }

        [Test]
        [TestCase(null, "Design Department", null, ExpectedResult = 2)]
        [TestCase(null, "Development Department", null, ExpectedResult = 2)]
        [TestCase(null, "Account Department", null, ExpectedResult = 0)]
        [TestCase(null, "Department", null, ExpectedResult = 5)]
        [TestCase(null, null, "Design", ExpectedResult = 2)]
        [TestCase("on", null, null, ExpectedResult = 2)]
        public async Task<int> GetEmployees_WithSearchParameters_ShouldBeCorrectCount(string name, string departmentName, string subDepartmentName)
        {
            //Execute
            var response = await _controller.GetEmployees(name, departmentName, subDepartmentName);

            //Assert
            Assert.NotNull(response);
            Assert.IsTrue(response is OkObjectResult);

            var result = response as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Value is List<Employee>);

            var body = (List<Employee>)result.Value;
            return body.Count;
        }

        [Test]
        [TestCase(null, "Account Department", null)]
        [TestCase(null, null, "Video Animation")]
        [TestCase("Oliver Queen", null, null)]
        [TestCase("Oliver Queen", "Account Department", "Video Animation")]
        public async Task GetEmployees_WithBadSearchParameters_ShouldReturnEmpty(string name, string departmentName, string subDepartmentName)
        {
            //Execute
            var response = await _controller.GetEmployees(name, departmentName, subDepartmentName);

            //Assert
            Assert.NotNull(response);
            Assert.IsTrue(response is OkObjectResult);

            var result = response as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Value is List<Employee>);

            var body = (List<Employee>)result.Value;
            Assert.IsEmpty(body);
        }
    }
}