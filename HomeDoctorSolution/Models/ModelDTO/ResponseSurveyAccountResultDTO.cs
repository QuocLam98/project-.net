namespace HomeDoctorSolution.Models.ModelDTO
{
    public class ResponseSurveyAccountResultDTO
    {
        public string? Text { get; set; }
        public string? Description { get; set; }
        public List<SurveySectionAccount> surveySectionAccounts { get; set; }

        public int? AccountId {get; set; }
    }
}
