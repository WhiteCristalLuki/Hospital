using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class ServicesHistory
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public int? ServiceId { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Service? Service { get; set; }
    }
}
