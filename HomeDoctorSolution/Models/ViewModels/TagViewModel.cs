namespace HomeDoctor.Models.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public int TagTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string TagName { get; set; }
        public int TagId { get; set; }
        public string TagTypeName { get; set; }
    }
}
