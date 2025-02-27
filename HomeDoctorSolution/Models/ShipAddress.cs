using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class ShipAddress
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? WardId { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }
        public string? AddressDetail { get; set; }
        public int AccountId { get; set; }
        public string RecipientName { get; set; } = null!;
        public string RecipientPhoneNumber { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual District? District { get; set; }
        public virtual Province? Province { get; set; }
        public virtual Ward? Ward { get; set; }
    }
}
