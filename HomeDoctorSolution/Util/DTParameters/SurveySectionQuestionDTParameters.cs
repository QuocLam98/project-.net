
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class SurveySectionQuestionDTParameters: DTParameters
            {
                public List<int> SurveySectionIds { get; set; } = new List<int>(); 
public List<int> QuestionIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    