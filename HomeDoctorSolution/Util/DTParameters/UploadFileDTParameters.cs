
        using System;
        using System.Collections.Generic;
        
        namespace HomeDoctorSolution.Util.Parameters
        {
            public class UploadFileDTParameters: DTParameters
            {
                public List<int> FolderUploadIds { get; set; } = new List<int>(); 
public List<int> AccountIds { get; set; } = new List<int>(); 

                public string SearchAll { get; set; } = "";
            }
        }
    