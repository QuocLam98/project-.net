
namespace HomeDoctorSolution.Models.ViewModels
{
    public class CartProductViewModel : CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string Action { get; set; }
        public string ProductName { get; set; }

        public string CartName { get; set; }

        public int AccountId { get; set; }

    }
}
