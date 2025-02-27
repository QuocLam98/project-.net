using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class RoleMenu
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
