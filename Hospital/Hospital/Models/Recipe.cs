using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            DiseasesHistories = new HashSet<DiseasesHistory>();
        }

        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public int? MedecineId { get; set; }
        public DateTime? PrescribeDate { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Medecine? Medecine { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual ICollection<DiseasesHistory> DiseasesHistories { get; set; }
    }
}
