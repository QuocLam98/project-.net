using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Service
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public decimal? Price { get; set; }
        public int HealthFacilityId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Photo { get; set; }
        public virtual HealthFacility HealthFacility { get; set; } = null!;
        public int? ParentId { get; set; }
        public string? Intro { get; set; }
    }
}
