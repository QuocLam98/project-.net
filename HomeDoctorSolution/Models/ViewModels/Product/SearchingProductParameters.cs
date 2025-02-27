namespace HomeDoctor.Models.ViewModels.Product
{
    public class SearchingProductParameters
    {
        public string Keywords { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public string CategoryProductId { get; set; } = "";
    }
}
