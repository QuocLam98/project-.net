using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class PostType
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
