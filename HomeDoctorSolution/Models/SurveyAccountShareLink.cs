using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class SurveyAccountShareLink
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string? Name { get; set; }
        public int? SenderAccountId { get; set; }
        public int? ReceiverAccountId { get; set; }
        public int? SurveyAccountId { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
