using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class BookingtMeta
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Booking Booking { get; set; } = null!;
    }
}
