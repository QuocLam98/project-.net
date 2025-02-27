
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ForumPostDTParameters: DTParameters
            {
                public List<int> ForumPostTypeIds { get; set; } = new List<int>(); 
public List<int> ForumPostStatusIds { get; set; } = new List<int>(); 
public List<int> ForumCategoryIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    