using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Info { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalShipFee { get; set; }
        public decimal? Tax { get; set; }
        public string? ShipCountryAddress { get; set; }
        public string? ShipProvinceAddress { get; set; }
        public string? ShipDistrictAddress { get; set; }
        public string? ShipWardAddress { get; set; }
        public string? ShipAddressDetail { get; set; }
        public string? ShipRecipientName { get; set; }
        public string? ShipRecipientPhone { get; set; }
        public int? ShipProvinceAddressId { get; set; }
        public int? ShipDistrictAddressId { get; set; }
        public int? ShipWardAddressId { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? PromotionId { get; set; }
        public string? LabelId { get; set; }
        public int OrderTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public int OrderPaymentStatusId { get; set; }
        public int? OrderStatusShipId { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual OrderPaymentStatus OrderPaymentStatus { get; set; } = null!;
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual OrderType OrderType { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
