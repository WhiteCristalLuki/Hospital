using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            //Recipes = new HashSet<Recipe>();
            //Departments = new HashSet<Department>();
            //Positions = new HashSet<Position>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
