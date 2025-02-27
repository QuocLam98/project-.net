
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class HomepageContentDTParameters: DTParameters
            {
                public List<int> HomepageContentTypeIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    