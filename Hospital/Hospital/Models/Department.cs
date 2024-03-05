using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Department
    {
        public Department()
        {
            //Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CorpsId { get; set; }

        public virtual Corps? Corps { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
