
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class NotificationDTParameters: DTParameters
            {
                public List<int> AccountIds { get; set; } = new List<int>(); 
public List<int> NotificationStatusIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    