
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class HomepageContentMetaDTParameters: DTParameters
            {
                public List<int> HomepageContentIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    