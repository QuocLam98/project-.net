using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int MessageTypeId { get; set; }
        public int MessageStatusId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Text { get; set; }
        public int RoomId { get; set; }
        public int AccountId { get; set; }
        public int ReceiverId { get; set; }
        public virtual Account Account { get; set; } = null!;
        public virtual MessageStatus MessageStatus { get; set; } = null!;
        public virtual MessageType MessageType { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
