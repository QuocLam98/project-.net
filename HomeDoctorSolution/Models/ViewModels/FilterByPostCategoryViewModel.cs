namespace HomeDoctor.Models.ViewModels
{
    public class FilterByPostCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountPost { get; set; }

        public int PostTypeId { get; set; }
    }
}
