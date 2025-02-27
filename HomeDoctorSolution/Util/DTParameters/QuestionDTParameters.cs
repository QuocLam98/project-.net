
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class QuestionDTParameters: DTParameters
            {
                public List<int> QuestionTypeIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    