using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingtMeta = new HashSet<BookingtMeta>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public int BookingTypeId { get; set; }
        public int BookingStatusId { get; set; }
        public int? CounselorId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Url { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public string? Info { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Reason { get; set; }
        public virtual Account Account { get; set; } = null!;
        public virtual BookingStatus BookingStatus { get; set; } = null!;
        public virtual BookingType BookingType { get; set; } = null!;
        public virtual Account? Counselor { get; set; }
        public virtual ICollection<BookingtMeta> BookingtMeta { get; set; }
        public virtual ICollection<Consultant> Consultant { get; set; }
        public int? ServiceId { get; set; }
        public string? Gender { get; set; }
        public string? DoB { get; set; }
        public string? Email { get; set; }
        public string? Guide { get;set; }
    }
}
