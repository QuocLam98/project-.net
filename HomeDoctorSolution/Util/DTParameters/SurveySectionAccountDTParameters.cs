
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class SurveySectionAccountDTParameters: DTParameters
            {
                public List<int> SurveyAccountIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    