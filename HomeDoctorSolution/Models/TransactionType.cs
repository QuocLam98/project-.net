using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Active { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
