using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? FinalPrice { get; set; }
        public int OrderDetailStatusId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual OrderDetailStatus OrderDetailStatus { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
