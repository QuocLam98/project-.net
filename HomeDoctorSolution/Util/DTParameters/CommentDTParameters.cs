
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class CommentDTParameters: DTParameters
            {
                public List<int> PostIds { get; set; } = new List<int>(); 
public List<int> AccountIds { get; set; } = new List<int>(); 
public List<int> CommentStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    