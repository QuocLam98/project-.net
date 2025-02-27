﻿using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Active { get; set; }
        public int Score { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Question Question { get; set; } = null!;
    }
}
