using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
            RoleMenus = new HashSet<RoleMenu>();
            RoleRight = new HashSet<RoleRight>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
        public virtual ICollection<RoleRight> RoleRight { get; set; }
    }
}
