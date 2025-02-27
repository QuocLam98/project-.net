
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class TransactionsDTParameters: DTParameters
            {
                public List<int> TransactionTypeIds { get; set; } = new List<int>(); 
public List<int> TransactionStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    