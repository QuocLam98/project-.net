
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class AnswerDTParameters: DTParameters
            {
                public List<int> QuestionIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    