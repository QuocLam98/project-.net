using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Product
    {
        public Product()
        {
            CartProducts = new HashSet<CartProduct>();
            OrderDetails = new HashSet<OrderDetail>();
        }

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
        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductStatusId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ProductBrand ProductBrand { get; set; } = null!;
        public virtual ProductCategory ProductCategory { get; set; } = null!;
        public virtual ProductStatus ProductStatus { get; set; } = null!;
        public virtual ProductType ProductType { get; set; } = null!;
        public virtual ICollection<CartProduct> CartProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
