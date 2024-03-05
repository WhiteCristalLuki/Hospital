using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Corps
    {
        public Corps()
        {
            Chambers = new HashSet<Chamber>();
            Departments = new HashSet<Department>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Chamber> Chambers { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
