namespace HomeDoctorSolution.Models.ModelDTO
{
    public class AccountAdminDTO
    {
        public int Id { get;set; }
        public int Active { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public int AccountStatusId { get; set; }
        public string AccountStatusName { get; set; }
        public int IsActivated { get; set; }
        public string? Info { get; set; }
        public string? Phone { get; set; }
        public string? Photo { get; set; }
        public string? Address { get; set; }
        public string? IdCardNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Name { get; set;}
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? ClassId { get; set; }   
        public int? SchoolId { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
