using HomeDoctorSolution.Models;

namespace HomeDoctor.Models.ModelDTO;

public class CartDetailViewModel : CartProduct
{
    
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Photo { get; set; }
    public int CartId { get; set; }
    public int Quantity { get; set; }
}