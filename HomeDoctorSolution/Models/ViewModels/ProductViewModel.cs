
using HomeDoctor.Models;

namespace HomeDoctorSolution.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? Info { get; set; }
        public string? Photo { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductStatusId { get; set; }
        public string ProductStatusName { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<ListPhoto>? ListPhotos { get; set; } = new List<ListPhoto>();
    }
    public class ListPhoto
    {
        public string Photo { get; set; }
        public string Description { get; set; }
    }
}
