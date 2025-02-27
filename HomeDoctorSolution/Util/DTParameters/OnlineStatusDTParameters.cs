
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class OnlineStatusDTParameters: DTParameters
            {
                public List<int> AccountIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    