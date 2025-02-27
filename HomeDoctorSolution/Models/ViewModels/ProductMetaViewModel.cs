using HomeDoctorSolution.Util;

namespace HomeDoctor.Models.ViewModels
{
    public class ProductMetaViewModel : DTParameters
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public string SearchAll { get; set; } = "";

    }
}
