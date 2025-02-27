using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class AccountMeta
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
