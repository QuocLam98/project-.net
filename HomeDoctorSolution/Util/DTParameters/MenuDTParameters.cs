
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class MenuDTParameters: DTParameters
            {
                public List<int> MenuTypeIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    