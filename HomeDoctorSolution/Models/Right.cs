using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Right
    {
        public Right()
        {
            RoleRight = new HashSet<RoleRight>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Url { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<RoleRight> RoleRight { get; set; }
    }
}
