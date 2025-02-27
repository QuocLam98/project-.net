
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ReadedPostDTParameters: DTParameters
            {
                public List<int> PostIDs { get; set; } = new List<int>(); 
public List<int> AccountIDs { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    