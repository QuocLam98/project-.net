using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Entity
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
