using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class MedicalProfile
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Info { get; set; }
        public string? Diagnose { get; set; }
        public string? HealthInfo { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Bmi { get; set; }
        public int? GradeId { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
