
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class SurveySectionAccountDetailDTParameters: DTParameters
            {
                public List<int> SurveySectionAccountIds { get; set; } = new List<int>(); 
public List<int> QuestionIds { get; set; } = new List<int>(); 
public List<int> AnswerIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    