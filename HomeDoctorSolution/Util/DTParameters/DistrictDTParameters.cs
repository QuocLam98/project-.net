
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class DistrictDTParameters: DTParameters
            {
                public List<int> ProvinceIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    