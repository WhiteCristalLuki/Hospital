using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Patient
    {
        public Patient()
        {
            DiseasesHistories = new HashSet<DiseasesHistory>();
            Recipes = new HashSet<Recipe>();
            ServicesHistories = new HashSet<ServicesHistory>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
        public int? ChamberId { get; set; }

        public virtual Chamber? Chamber { get; set; }
        public virtual ICollection<DiseasesHistory> DiseasesHistories { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<ServicesHistory> ServicesHistories { get; set; }
    }
}
