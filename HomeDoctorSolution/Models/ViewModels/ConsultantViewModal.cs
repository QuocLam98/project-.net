namespace HomeDoctorSolution.Models.ViewModel
{
    public class ConsultantViewModal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public int CounselorId { get; set; }
        public string CounselorName { get; set; }
        public int BookingId { get; set; }
        public int BookingTypeId { get; set; }
        public string BookingTypeName { get; set; }
        public string Description { get; set; }
        public string? Phone { get; set; }
        public string Reason { get; set; }
        public string Symptom { get; set; }
        public string Religiorelationship { get; set; }
        public string History { get; set; }
        public string AssessmentResult { get; set; }
        public string Target { get; set; }
        public string ConsultingTime { get; set; }
        public string ConsultingResults { get; set; }
        public string ConsultingPlan { get; set; }
        public string ReligiousNation { get; set; }
        public string CulturalLevel { get; set; }
        public string Implementation { get; set; }
        public string? Form { get; set; }
        public double? Rating { get; set; }
        public string? Review { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
