using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class HealthFacilityStatus
    {
        public HealthFacilityStatus()
        {
            HealthFacilities = new HashSet<HealthFacility>();
        }

        public int Id { get; set; }
        public int? Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<HealthFacility> HealthFacilities { get; set; }
    }
}
