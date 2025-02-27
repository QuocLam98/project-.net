namespace HomeDoctor.Models
{
    public class ProductMeta
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } 
        public int Active { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
