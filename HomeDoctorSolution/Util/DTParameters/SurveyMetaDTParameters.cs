
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class SurveyMetaDTParameters: DTParameters
            {
                public List<int> SurveyIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    