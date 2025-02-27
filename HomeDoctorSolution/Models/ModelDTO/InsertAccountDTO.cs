namespace HomeDoctorSolution.Models.ModelDTO
{
	public class InsertAccountDTO
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? MiddleName { get; set; }
		public string Email { get; set; } = null!;
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? Photo {  get; set; } = null!;
    }
}
