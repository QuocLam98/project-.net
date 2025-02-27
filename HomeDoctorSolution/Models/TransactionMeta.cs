using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class TransactionMeta
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
        public string? Description { get; set; }
        public int Active { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Transaction Transaction { get; set; } = null!;
    }
}
