using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class DiseasesHistory
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public DateTime? DiseaseDate { get; set; }
        public int? DiseaseId { get; set; }
        public string? Notes { get; set; }
        public int? RecipeId { get; set; }

        public virtual Disease? Disease { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
