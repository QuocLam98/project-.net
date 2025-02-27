
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class MessageDTParameters: DTParameters
            {
                public List<int> MessageTypeIds { get; set; } = new List<int>(); 
public List<int> MessageStatusIds { get; set; } = new List<int>(); 
public List<int> RoomIds { get; set; } = new List<int>(); 
public List<int> AccountIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    