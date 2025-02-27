using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? Photo { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
