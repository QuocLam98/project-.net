
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class OrderDetailDTParameters: DTParameters
            {
                public List<int> ProductIds { get; set; } = new List<int>(); 
public List<int> OrderDetailStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    