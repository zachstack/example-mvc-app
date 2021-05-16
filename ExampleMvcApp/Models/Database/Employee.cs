using System;
using System.Collections.Generic;

#nullable disable

namespace ExampleMvcApp.Models.Database
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public int SubDepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public string FbprofileLink { get; set; }
        public string TwitterProfileLink { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual SubDepartment SubDepartment { get; set; }
    }
}
