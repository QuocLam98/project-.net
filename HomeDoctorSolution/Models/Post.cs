using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Post
    {
        public Post()
        {
            FavouritePosts = new HashSet<FavouritePost>();
            FeaturedPosts = new HashSet<FeaturedPost>();
            PostMeta = new HashSet<PostMeta>();
            PostTags = new HashSet<PostTag>();
            ReadedPosts = new HashSet<ReadedPost>();
        }

        public int Id { get; set; }
        public int PostTypeId { get; set; }
        public int PostAccountId { get; set; }
        public int PostCategoryId { get; set; }
        public int? PostLayoutId { get; set; }
        public int PostPublishStatusId { get; set; }
        public int PostCommentStatusId { get; set; }
        public int AuthorId { get; set; }
        public string? GuId { get; set; }
        public string? Photo { get; set; }
        public string? Video { get; set; }
        public int? ViewCount { get; set; }
        public int? CommentCount { get; set; }
        public int? LikeCount { get; set; }
        public int Active { get; set; }
        public string Url { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public string? DownloadLink { get; set; }
        public DateTime PublishedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual PostCategory PostCategory { get; set; } = null!;
        public virtual PostCommentStatus PostCommentStatus { get; set; } = null!;
        public virtual PostLayout? PostLayout { get; set; }
        public virtual PostPublishStatus PostPublishStatus { get; set; } = null!;
        public virtual PostType PostType { get; set; } = null!;
        public virtual ICollection<FavouritePost> FavouritePosts { get; set; }
        public virtual ICollection<FeaturedPost> FeaturedPosts { get; set; }
        public virtual ICollection<PostMeta> PostMeta { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<ReadedPost> ReadedPosts { get; set; }
    }
}
