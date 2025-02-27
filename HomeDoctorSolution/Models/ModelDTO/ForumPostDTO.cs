namespace HomeDoctorSolution.Models.ModelDTO
{
    public class ForumPostDTO
    {
        public int Id { get; set; }
        public int ForumCategoryId { get; set; }
        public int ForumPostTypeId { get; set; }
        public string? Photo { get; set; }
        public string Name { get; set; } = null!;
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? Text { get; set; }
    }
}
