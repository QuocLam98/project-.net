using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
            ShipAddresses = new HashSet<ShipAddress>();
        }

        public int Id { get; set; }
        //public int CountryId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        //public virtual Country Country { get; set; } = null!;
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
    }
}
