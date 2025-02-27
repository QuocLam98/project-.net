using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class HealthFacility
    {
        public HealthFacility()
        {
            Doctor = new HashSet<Doctor>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime FoundedYear { get; set; }
        public int DistrictId { get; set; }
        public string? Linkmap { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Info { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public int HealthFacilityStatusId { get; set; }
        public string? AddressDetail { get; set; }
        public string? OpenDate { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public virtual HealthFacilityStatus HealthFacilityStatus { get; set; } = null!;
        public virtual HealthFacilityType HealthFacilityType { get; set; } = null!;
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public int? ProvinceId { get; set; }
    }
}
