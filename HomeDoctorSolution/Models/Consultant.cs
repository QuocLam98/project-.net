using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Consultant
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? DoctorId { get; set; }
        public string? Reason { get; set; }
        public string? Symptom { get; set; }
        public string? Religiorelationship { get; set; }
        public string? History { get; set; }
        public string? AssessmentResult { get; set; }
        public string? Target { get; set; }
        public string? ConsultingResults { get; set; }
        public string? ConsultingPlan { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? Description { get; set; }
        public string? ReligiousNation { get; set; }
        public string? CulturalLevel { get; set; }
        public string? Implementation { get; set; }
        public int BookingId { get; set; }
        public string? ConsultingTime { get; set; }
        public string? Form { get; set; }
        public double? Rating { get; set; }
        public string? Review { get; set; }
    }
}
