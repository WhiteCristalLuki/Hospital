using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Position
    {
        public Position()
        {
            staff = new HashSet<Staff>();
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Salary { get; set; }
        public virtual ICollection<Staff> staff { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
