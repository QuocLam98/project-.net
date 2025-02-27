using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class SurveyAccount
    {
        public SurveyAccount()
        {
            SurveySectionAccounts = new HashSet<SurveySectionAccount>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public int SurveyId { get; set; }
        public int Active { get; set; }
        public int Score { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime StartDate { get; set; }
        public string? Url { get; set; }

        public virtual Survey Survey { get; set; } = null!;
        public virtual ICollection<SurveySectionAccount> SurveySectionAccounts { get; set; }
    }
}
