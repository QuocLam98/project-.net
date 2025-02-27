using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            SurveySectionAccountDetails = new HashSet<SurveySectionAccountDetail>();
            SurveySectionQuestions = new HashSet<SurveySectionQuestion>();
        }

        public int Id { get; set; }
        public int? QuestionTypeId { get; set; }
        public int Active { get; set; }
        public string? Photo { get; set; }
        public int Score { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual QuestionType? QuestionType { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<SurveySectionAccountDetail> SurveySectionAccountDetails { get; set; }
        public virtual ICollection<SurveySectionQuestion> SurveySectionQuestions { get; set; }
    }
}
