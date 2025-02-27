
namespace HomeDoctorSolution.Models.ViewModels
{
    public class MedicalProfileViewModel : MedicalProfile
    {

        public string AccountName { get; set; }
        public string? Phone { get; set; }
        public int? HeartDiseaseCard { get; set; }
        public int? Diabetes { get; set; }
        public int? Asthma { get; set; }
        public int? Epilepsy { get; set; }
        public int? Depression { get; set; }
        public int? Stress { get; set; }
        public int? AnxietyDisorders { get; set; }
        public string? Orther { get; set; }

    }
}
