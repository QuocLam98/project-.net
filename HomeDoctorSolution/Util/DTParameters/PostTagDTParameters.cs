
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class PostTagDTParameters: DTParameters
            {
                public List<int> PostIds { get; set; } = new List<int>(); 
public List<int> TagIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    