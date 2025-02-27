
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class ChatbotMessageDTParameters: DTParameters
            {
                public List<int> ChatbotTopicIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    