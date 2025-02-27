
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class BookingtMetaDTParameters: DTParameters
            {
                public List<int> BookingIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    