using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Country
    {
        public Country()
        {
            //Provinces = new HashSet<Province>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        //public virtual ICollection<Province> Provinces { get; set; }
    }
}
