using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountMeta = new HashSet<AccountMeta>();
            ActivityLogs = new HashSet<ActivityLog>();
            Anamnesis = new HashSet<Anamnesis>();
            Authors = new HashSet<Author>();
            Carts = new HashSet<Cart>();
            Doctor = new HashSet<Doctor>();
            MedicalProfiles = new HashSet<MedicalProfile>();
            Messages = new HashSet<Message>();
            OnlineStatuses = new HashSet<OnlineStatus>();
            ShipAddresses = new HashSet<ShipAddress>();
            UploadFiles = new HashSet<UploadFiles>();
            Orders = new HashSet<Order>();

        }

        public int Id { get; set; }
        public string? GuId { get; set; }
        public int RoleId { get; set; }
        public int AccountTypeId { get; set; }
        public int AccountStatusId { get; set; }
        public int Active { get; set; }
        public int IsActivated { get; set; }
        public string Name { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string? Info { get; set; }
        public string? GoogleId { get; set; }
        public string? AppleId { get; set; }
        public string? FacebookId { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? IdCardNumber { get; set; }
        public string? IdCardPhoto1 { get; set; }
        public string? IdCardPhoto2 { get; set; }
        public string? IdCardGrantedDate { get; set; }
        public string? IdCardGrantedPlace { get; set; }
        public string? Zipcode { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressDistrict { get; set; }
        public string? AddressWard { get; set; }
        public string? AddressDetail { get; set; }
        public string? LinkedAccount { get; set; }
        public string? LinkedPassword { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string? BankBranch { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? BankNote { get; set; }
        public DateTime CreatedTime { get; set; }
        public int? ClassId { get; set; }
        public int? SchoolId { get; set; }

        public virtual AccountStatus AccountStatus { get; set; } = null!;
        public virtual AccountType AccountType { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<AccountMeta> AccountMeta { get; set; }
        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
        public virtual ICollection<Anamnesis> Anamnesis { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<MedicalProfile> MedicalProfiles { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<OnlineStatus> OnlineStatuses { get; set; }
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
        public virtual ICollection<UploadFiles> UploadFiles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
