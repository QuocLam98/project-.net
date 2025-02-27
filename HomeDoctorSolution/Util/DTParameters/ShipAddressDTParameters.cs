
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ShipAddressDTParameters: DTParameters
            {
                public List<int> WardIds { get; set; } = new List<int>(); 
public List<int> DistrictIds { get; set; } = new List<int>(); 
public List<int> ProvinceIds { get; set; } = new List<int>(); 
public List<int> AccountIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    