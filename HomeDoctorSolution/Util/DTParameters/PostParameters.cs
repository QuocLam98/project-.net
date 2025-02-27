namespace HomeDoctor.Util.DTParameters
{
    public class PostParameters
    {
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public string Search { get; set; }
        public string PostName { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int PostTypeId { get; set; }
        public int AuthorId { get; set; }
        public string PostType { get; set; } = "";
        public string ListTag { get; set; } = "";
        public int TagId { get; set; }
        public string PostCategory { get; set; } = "";
        public int PostCategoryId { get; set; }
        public int PostTagId { get; set; }
    }
}
