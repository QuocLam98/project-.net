using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class District
    {
        public District()
        {
            ShipAddresses = new HashSet<ShipAddress>();
            Wards = new HashSet<Ward>();
        }

        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Province Province { get; set; } = null!;
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
