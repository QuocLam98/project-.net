using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class RoleRight
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RightsId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Right Right { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
