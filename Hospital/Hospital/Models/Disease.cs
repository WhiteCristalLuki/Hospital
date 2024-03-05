using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Disease
    {
        public Disease()
        {
            DiseasesHistories = new HashSet<DiseasesHistory>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<DiseasesHistory> DiseasesHistories { get; set; }
    }
}
