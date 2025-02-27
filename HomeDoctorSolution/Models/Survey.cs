using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Survey
    {
        public Survey()
        {
            SurveyAccounts = new HashSet<SurveyAccount>();
            SurveyMeta = new HashSet<SurveyMeta>();
            SurveySections = new HashSet<SurveySection>();
        }

        public int Id { get; set; }
        public int SurveyTypeId { get; set; }
        public int SurveyStatusId { get; set; }
        public string? Photo { get; set; }
        public string? Video { get; set; }
        public int? ViewCount { get; set; }
        public int? CommentCount { get; set; }
        public int? LikeCount { get; set; }
        public int Active { get; set; }
        public string? Url { get; set; }
        public int Score { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual SurveyStatus SurveyStatus { get; set; } = null!;
        public virtual SurveyType SurveyType { get; set; } = null!;
        public virtual ICollection<SurveyAccount> SurveyAccounts { get; set; }
        public virtual ICollection<SurveyMeta> SurveyMeta { get; set; }
        public virtual ICollection<SurveySection> SurveySections { get; set; }
    }
}
