
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class WardDTParameters: DTParameters
            {
                public List<int> DistrictIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    