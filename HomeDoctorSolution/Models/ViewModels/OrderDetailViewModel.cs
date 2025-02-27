
namespace HomeDoctorSolution.Models.ViewModels
{
    public class OrderDetailViewModel : OrderDetail
    {

        public string ProductName { get; set; }
        public string ProductPhoto { get; set; }
        public int Quantity { get; set; }
        // public decimal? Price { get; set; }
        public decimal? FinalPrice { get; set; }
        public string OrderDetailStatusName { get; set; }

    }
}
