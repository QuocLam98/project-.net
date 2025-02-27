
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class CartProductDTParameters: DTParameters
            {
                public List<int> ProductIds { get; set; } = new List<int>(); 
public List<int> CartIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    