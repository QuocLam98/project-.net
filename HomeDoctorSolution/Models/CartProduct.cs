using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class CartProduct
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
