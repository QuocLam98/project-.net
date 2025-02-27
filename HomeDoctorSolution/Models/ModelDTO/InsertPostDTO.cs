namespace HomeDoctorSolution.Models.ModelDTO
{
    public class InsertPostDTO
    {
        public int PostTypeId { get; set; }
        public int PostAccountId { get; set; }
        public int PostStatusId { get; set; }
        public int PostCategoryId { get; set; }
        public int PostPublishStatusId { get; set; }
        public int PostCommentStatusId { get; set; }
        public string Name { get; set; } 
        public string Url { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public int AuthorId { get; set; }
        public int? ViewCount { get; set; }
        public List<string> TagIds { get; set; }
        public DateTime PublishedTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
