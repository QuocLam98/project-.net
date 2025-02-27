namespace HomeDoctorSolution.Models.ModelDTO
{
    public class AccountProfileResponseDTO
    {
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        public string Username { get; set; } = null!;
        public string? Phone { get; set; }
        public int RoleId { get; set; }
    }
}
