using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class OnlineStatus
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Data { get; set; }
        public string? Description { get; set; }
        public int IsOnline { get; set; }
        public DateTime LastOnlineTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
