using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class OrderStatusShip
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
