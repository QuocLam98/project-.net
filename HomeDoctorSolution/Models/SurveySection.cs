using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class SurveySection
    {
        public SurveySection()
        {
            SurveySectionQuestions = new HashSet<SurveySectionQuestion>();
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int? ParentId { get; set; }
        public int Active { get; set; }
        public int ProportionScore { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Survey Survey { get; set; } = null!;
        public virtual ICollection<SurveySectionQuestion> SurveySectionQuestions { get; set; }
    }
}
