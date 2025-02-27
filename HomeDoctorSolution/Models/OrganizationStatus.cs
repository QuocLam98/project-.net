using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class OrganizationStatus
    {
        public OrganizationStatus()
        {
            Organizations = new HashSet<Organization>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
