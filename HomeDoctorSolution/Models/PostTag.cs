using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class PostTag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
