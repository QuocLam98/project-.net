using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Email
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public string Source { get; set; } = null!;
        public string? Text { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
