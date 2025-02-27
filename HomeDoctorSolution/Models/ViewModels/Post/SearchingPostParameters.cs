namespace HomeDoctor.Models.ViewModels.Post
{
    public class SearchingPostParameters
    {
        public string Keywords { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public string CategoryPostId { get; set; } = "";
        public string PostCategory { get; set; } = "";
    }
}
