using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public int AccountId { get; set; }
        public int NotificationStatusId { get; set; }
        public string Name { get; set; } = null!;
        public int? SenderId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? LinkDetail { get; set; }

        public virtual NotificationStatus NotificationStatus { get; set; } = null!;
    }
}
