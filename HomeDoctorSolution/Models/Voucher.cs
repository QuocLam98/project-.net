using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public int? Value { get; set; }
        public string? Quantity { get; set; }
        public int? PromotionId { get; set; }
        public int VoucherStatusId { get; set; }
        public int VoucherTypeId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Promotion? Promotion { get; set; }
        public virtual VoucherStatus VoucherStatus { get; set; } = null!;
        public virtual VoucherType VoucherType { get; set; } = null!;
    }
}
