using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public int ContactStatusId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Message { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ContactStatus ContactStatus { get; set; } = null!;
    }
}
