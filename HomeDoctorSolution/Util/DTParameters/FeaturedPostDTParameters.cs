
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class FeaturedPostDTParameters: DTParameters
            {
                public List<int> PostIds { get; set; } = new List<int>(); 
public List<int> FeaturedPostTypeIds { get; set; } = new List<int>(); 
//public List<int> FeaturedPostTypeIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    