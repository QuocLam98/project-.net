using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ViewModels;

namespace HomeDoctor.Models.ViewModels.Product;

public class OrderViewModel : Order
{
    public int Id { get; set; } 
    public decimal? TotalPrice { get; set; }
    public decimal? TotalShipFee { get; set; }
    public decimal? Tax { get; set; }
    public decimal? FinalPrice { get; set; }
    public int? ShipProvinceAddressId { get; set; }
    public int? ShipDistrictAddressId { get; set; }
    public int? ShipWardAddressId { get; set; }
    public string? Phone { get; set; }
    public string Name { get; set; } = null!;
    
    public List<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
}