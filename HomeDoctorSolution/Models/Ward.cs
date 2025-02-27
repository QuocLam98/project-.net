using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Ward
    {
        public Ward()
        {
            ShipAddresses = new HashSet<ShipAddress>();
        }

        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual District District { get; set; } = null!;
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
    }
}
