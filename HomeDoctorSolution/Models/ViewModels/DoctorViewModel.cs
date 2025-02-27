using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Doctors
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Active { get; set; }
        public string? Description { get; set; }
        public string? Info { get; set; }
        public string? WorkAddress { get; set; }
        public string? Position { get; set; }
        public string? Experience { get; set; }
        public string? Specialist { get; set; }
        public string? Language { get; set; }
        public string? License { get; set; }
        public int AccountId { get; set; }
        public int DoctorTypeId { get; set; }
        public int DoctorStatusId { get; set; }
        public int HealthFacilityId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Account { get; set; }
        public string DoctorType { get; set; }
        public string DoctorStatus { get; set; }
        public string HealthFacility { get; set; }

    }
}