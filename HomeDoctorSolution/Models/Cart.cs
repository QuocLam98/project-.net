using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartProducts = new HashSet<CartProduct>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}
