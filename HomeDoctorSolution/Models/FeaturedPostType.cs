using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class FeaturedPostType
    {
        public FeaturedPostType()
        {
            FeaturedPosts = new HashSet<FeaturedPost>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<FeaturedPost> FeaturedPosts { get; set; }
    }
}
