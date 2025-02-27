using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Author
    {
        public Author()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }
    }
}
