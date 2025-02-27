
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class RoleRightsDTParameters: DTParameters
            {
                public List<int> RoleIds { get; set; } = new List<int>(); 
public List<int> RightsIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    