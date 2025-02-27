using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class PromotionMeta
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
        public string? Description { get; set; }
        public int Active { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Promotion Promotion { get; set; } = null!;
    }
}
