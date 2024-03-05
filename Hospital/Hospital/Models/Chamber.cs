using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Chamber
    {
        public Chamber()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Gender { get; set; }
        public int? Capacity { get; set; }
        public int? Availability { get; set; }
        public int? CorpsId { get; set; }

        public virtual Corps? Corps { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
