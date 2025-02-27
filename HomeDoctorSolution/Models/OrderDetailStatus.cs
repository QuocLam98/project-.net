using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class OrderDetailStatus
    {
        public OrderDetailStatus()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
