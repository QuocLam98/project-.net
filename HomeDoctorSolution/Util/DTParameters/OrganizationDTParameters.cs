
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class OrganizationDTParameters: DTParameters
            {
                public List<int> OrganizationTypeIds { get; set; } = new List<int>(); 
public List<int> OrganizationStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    