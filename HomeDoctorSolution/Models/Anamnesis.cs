using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Anamnesis
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public int? HeartDiseaseCard { get; set; }
        public int? Diabetes { get; set; }
        public int? Asthma { get; set; }
        public int Epilepsy { get; set; }
        public int AccountId { get; set; }
        public int? Depression { get; set; }
        public int? Stress { get; set; }
        public string? Orther { get; set; }
        public int? AnxietyDisorders { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
