using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class SurveySectionQuestion
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveySectionId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual SurveySection SurveySection { get; set; } = null!;
    }
}
