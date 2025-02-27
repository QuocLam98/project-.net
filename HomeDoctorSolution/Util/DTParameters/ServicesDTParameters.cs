
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ServicesDTParameters: DTParameters
            {
                public List<int> HealthFacilityIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    