
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ProvinceDTParameters: DTParameters
            {
                public List<int> CountryIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    