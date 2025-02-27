
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class HealthFacilityDTParameters : DTParameters
    {
        public List<int> HealthFacilityTypeIds { get; set; } = new List<int>();
        public List<int> HealthFacilityStatusIds { get; set; } = new List<int>();

        public string SearchAll { get; set; } = "";
    }
}
