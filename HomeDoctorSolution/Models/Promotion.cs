using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionMeta = new HashSet<PromotionMeta>();
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string? Code { get; set; }
        public decimal? Discount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<PromotionMeta> PromotionMeta { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
