using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class FeaturedPost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int FeaturedPostTypeId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual FeaturedPostType FeaturedPostType { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
