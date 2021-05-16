using ExampleMvcApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMvcApp.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string SubDepartmentName { get; set; }

        public List<Employee> Employees { get; set; }

        public EmployeeViewModel(List<Employee> employees, string name, string departmentName, string subDepartmentName)
        {
            Employees = employees;
            Name = name;
            DepartmentName = departmentName;
            SubDepartmentName = subDepartmentName;
        }
    }
}
