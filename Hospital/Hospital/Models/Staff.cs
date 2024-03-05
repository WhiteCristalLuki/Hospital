using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public partial class Staff
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public int? PositionId { get; set; }

        public virtual Position? Position { get; set; }
    }
}
