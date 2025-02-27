using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public int? PostCount { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
