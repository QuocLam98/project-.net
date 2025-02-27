
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class AccountDTParameters: DTParameters
            {
                public List<int> RoleIds { get; set; } = new List<int>(); 
public List<int> AccountTypeIds { get; set; } = new List<int>(); 
public List<int> AccountStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    