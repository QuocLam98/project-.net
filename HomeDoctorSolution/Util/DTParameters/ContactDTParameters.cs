
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ContactDTParameters: DTParameters
            {
                public List<int> ContactStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    