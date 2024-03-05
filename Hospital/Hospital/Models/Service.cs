using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Service
    {
        public Service()
        {
            ServicesHistories = new HashSet<ServicesHistory>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CorpsId { get; set; }

        public virtual Corps? Corps { get; set; }
        public virtual ICollection<ServicesHistory> ServicesHistories { get; set; }
    }
}
