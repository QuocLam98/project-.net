using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Organization
    {
        public int Id { get; set; }
        public int OrganizationTypeId { get; set; }
        public int OrganizationStatusId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual OrganizationStatus OrganizationStatus { get; set; } = null!;
        public virtual OrganizationType OrganizationType { get; set; } = null!;
    }
}
