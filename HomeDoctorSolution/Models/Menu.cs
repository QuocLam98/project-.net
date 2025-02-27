using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Menu
    {
        public Menu()
        {
            RoleMenus = new HashSet<RoleMenu>();
        }

        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? ParentId { get; set; }
        public int Active { get; set; }
        public int Priority { get; set; }
        public int MenuTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Url { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual MenuType MenuType { get; set; } = null!;
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
