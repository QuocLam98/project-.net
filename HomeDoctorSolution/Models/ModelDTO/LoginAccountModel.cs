namespace HomeDoctorSolution.Models.ModelDTO
{
    public class LoginAccountModel
    {
        public string Fullname { get; set; } = null!;
        public string Name { get; set; }
        public string? Phone { get; set; }

        public string? Photo { get; set; }

        public string? Token { get; set; }
    }
}
