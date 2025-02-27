
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class VoucherDTParameters: DTParameters
            {
                public List<int> PromotionIds { get; set; } = new List<int>(); 
public List<int> VoucherStatusIds { get; set; } = new List<int>(); 
public List<int> VoucherTypeIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    