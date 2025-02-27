
namespace HomeDoctorSolution.Models.ViewModels
{
    public class OrdersViewModel : Order
    {

        public string OrderTypeName { get; set; }

        public string OrderStatusName { get; set; }

        public string OrderPaymentStatusName { get; set; }

        public string OrderStatusShipName { get; set; }

        public string PromotionName { get; set; }
        public string AccountName { get; set; }

        public string AccountPhoto { get; set; }

        public string AccountPhone { get; set; }
        public string? ShipProvinceAddress { get; set; }
        public string? ShipDistrictAddress { get; set; }
        public string? ShipWardAddress { get; set; }
        public string? Description { get; set; }


        public List<OrderDetailViewModel> OrderDetails { get; set; }


    }
}
