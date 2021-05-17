using System;
using System.Collections.Generic;

#nullable disable

namespace ExampleMvcApp.Models.Database
{
    public partial class Employee
    {
        public Employee()
        {

        }

        public Employee(EmployeeAndNames emp)
        {
            EmployeeId = emp.EmployeeId;
            SubDepartmentId = emp.SubDepartmentId;
            FirstName = emp.FirstName;
            LastName = emp.LastName;
            Bio = emp.Bio;
            ProfileImage = emp.ProfileImage;
            FbprofileLink = emp.FbprofileLink;
            TwitterProfileLink = emp.TwitterProfileLink;
            AddedDate = emp.AddedDate;
            UpdatedDate = emp.UpdatedDate;
            Deleted = emp.Deleted;
            DeletedDate = emp.DeletedDate;
            SubDepartment = new SubDepartment()
            {
                SubDepartmentName = emp.SubDepartmentName,
                Department = new Department()
                {
                    DepartmentName = emp.DepartmentName
                }
            };
        }
    }
}
