namespace HomeDoctorSolution.Models.ModelDTO
{
	public class AccountProfileDTO
	{
		public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string Name { get; set; } = null!;
		public string AccountName {  get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? MiddleName { get; set; }
		public string? Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? Phone { get; set; }
		public string? Address { get; set; }
		public string? Photo { get; set; }
		public string? Description { get; set; }
		public string? Info { get; set; }
		public string? GoogleId { get; set; }
		public string? FacebookId { get; set; }
		public DateTime? Dob { get; set; }
		public string? Gender { get; set; }
		public string? IdCardNumber { get; set; }
		public string? IdCardGrantedDate { get; set; }
		public string? IdCardGrantedPlace { get; set; }
		public string? AddressCity { get; set; }
		public string? AddressDistrict { get; set; }
		public string? AddressWard { get; set; }
		public string? AddressDetail { get; set; }
		public string? AddressCityName {get; set; }
		public string? AddressDistrictName { get;set; }
		public string? AddressWardName { get; set; }

    }
}
