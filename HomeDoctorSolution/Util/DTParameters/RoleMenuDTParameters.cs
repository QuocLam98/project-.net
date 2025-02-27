
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class RoleMenuDTParameters: DTParameters
            {
                public List<int> RoleIds { get; set; } = new List<int>(); 
public List<int> MenuIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    