
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class AnamnesisDTParameters: DTParameters
            {
                public List<int> MedicalProfileIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    