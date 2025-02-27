using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AccountId { get; set; }
        public int Active { get; set; }
        public int CommentStatusId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual CommentStatus CommentStatus { get; set; } = null!;
    }
}
