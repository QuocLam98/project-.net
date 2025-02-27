
namespace HomeDoctorSolution.Models.ViewModels
{
    public class DoctorsViewModel : Doctor
    {

        public string AccountName { get; set; }
        public string Address { get; set; }

        public string DoctorTypeName { get; set; }

        public string DoctorStatusName { get; set; }

        public string HealthFacilityName { get; set; }
        public string? Image { get; set; }

        public string? ServiceName { get; set; }    
        public string? HealthFacilityAddress { get; set; }
        public decimal? ServiceFee { get; set; }

        public int? BookingId { get; set; }
    }
}
