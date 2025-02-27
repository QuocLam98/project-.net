using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionMeta = new HashSet<TransactionMeta>();
        }

        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public int TransactionStatusId { get; set; }
        public int? ServicesId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public int Amount { get; set; }
        public string? SenderInfo { get; set; }
        public string? ReceiveInfo { get; set; }
        public int OrderId { get; set; }
        public int Active { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual TransactionStatus TransactionStatus { get; set; } = null!;
        public virtual TransactionType TransactionType { get; set; } = null!;
        public virtual ICollection<TransactionMeta> TransactionMeta { get; set; }
    }
}
