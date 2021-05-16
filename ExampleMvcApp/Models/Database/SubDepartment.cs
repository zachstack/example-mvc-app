using System;
using System.Collections.Generic;

#nullable disable

namespace ExampleMvcApp.Models.Database
{
    public partial class SubDepartment
    {
        public SubDepartment()
        {
            Employees = new HashSet<Employee>();
        }

        public int SubDepartmentId { get; set; }
        public int DepartmentId { get; set; }
        public string SubDepartmentName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
