
using HomeDoctor.Models.ViewModels;

namespace HomeDoctorSolution.Models.ViewModels
{
    public class PostViewModel : Post
    {

        public int Id { get; set; }
        public int PostTypeId { get; set; }
        public int PostAccountId { get; set; }
        public int PostCategoryId { get; set; }
        public int? PostLayoutId { get; set; }
        public int PostPublishStatusId { get; set; }
        public int PostCommentStatusId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string? AuthorImage { get; set; }
        public string? AuthorUserName { get; set; }
        public string? GuId { get; set; }
        public string? Photo { get; set; }
        public string? Video { get; set; }
        public string? Username { get; set; }
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
        public string PostTypeName { get; set; }

        public int TagId { get; set; }
        public string TagName { get; set; }

        public string PostCategoryName { get; set; }

        public string PostLayoutName { get; set; }

        public string PostPublishStatusName { get; set; }

        public ICollection<TagViewModel> ListTag { get; set; } = new List<TagViewModel>();


    }
}
