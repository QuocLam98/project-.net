namespace HomeDoctorSolution.Models.ModelDTO
{
    public class ForgotPasswordDTO: ChangePasswordDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
    }
}
