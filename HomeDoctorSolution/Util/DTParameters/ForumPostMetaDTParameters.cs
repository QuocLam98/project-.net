
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ForumPostMetaDTParameters: DTParameters
            {
                public List<int> ForumPostIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    