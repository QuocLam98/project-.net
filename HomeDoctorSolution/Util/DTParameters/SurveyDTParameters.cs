
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class SurveyDTParameters: DTParameters
            {
                public List<int> SurveyTypeIds { get; set; } = new List<int>(); 
public List<int> SurveyStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    