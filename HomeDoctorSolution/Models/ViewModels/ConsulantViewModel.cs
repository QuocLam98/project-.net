namespace HomeDoctorSolution.Models.ViewModel
{
    public class ConsulantViewModel
    {
        public int CounselorsId { get; set; }
        public string CounselorsName { get; set; }
        public string? Photo { get; set; }
        public double? Rating { get; set; }
        public string? Review { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool HasNext { get; set; } = true;
        public int Total { get; set; }
    }

}
