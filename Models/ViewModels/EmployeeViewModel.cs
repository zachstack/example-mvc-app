using ExampleMvcApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMvcApp.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public List<Employee> Employees { get; set; }

        public EmployeeViewModel(List<Employee> employees)
        {
            Employees = employees;
        }
    }
}
