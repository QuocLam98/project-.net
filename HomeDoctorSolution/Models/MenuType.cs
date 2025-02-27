using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class MenuType
    {
        public MenuType()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
