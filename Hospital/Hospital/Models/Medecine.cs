using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Medecine
    {
        public Medecine()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Quantity { get; set; }
        public string? MakersCountry { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
