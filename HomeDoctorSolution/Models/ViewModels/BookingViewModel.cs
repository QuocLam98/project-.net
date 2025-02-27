
namespace HomeDoctorSolution.Models.ViewModels
{
    public class BookingViewModel : Booking
    {

        public string AccountName { get; set; }

        public string CounselorName { get; set; }

        public string BookingTypeName { get; set; }

        public string BookingStatusName { get; set; }
        public string ServiceName { get; set; }

        public decimal? Price { get;set; }
    }
}
