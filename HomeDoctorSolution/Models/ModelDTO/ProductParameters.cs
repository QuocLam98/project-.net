namespace HomeDoctor.Models.ModelDTO
{
    public class ProductParameters
    {
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public string Search { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int ProductBrandId { get; set; }
    }
}
