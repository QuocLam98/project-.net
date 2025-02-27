
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class DoctorsDTParameters : DTParameters
    {
        public List<int> AccountIds { get; set; } = new List<int>();
        public List<int> DoctorTypeIds { get; set; } = new List<int>();
        public List<int> DoctorStatusIds { get; set; } = new List<int>();
        public List<int> HealthFacilityIds { get; set; } = new List<int>();

        public string SearchAll { get; set; } = "";
    }
}
