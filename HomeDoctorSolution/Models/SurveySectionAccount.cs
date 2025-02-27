using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class SurveySectionAccount
    {
        public int Id { get; set; }
        public int SurveyAccountId { get; set; }
        public int Active { get; set; }
        public int Score { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual SurveyAccount SurveyAccount { get; set; } = null!;
    }
}
