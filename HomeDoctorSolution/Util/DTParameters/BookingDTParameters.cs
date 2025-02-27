
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class BookingDTParameters : DTParameters
    {
        public List<int> AccountIds { get; set; } = new List<int>();
        public List<int> BookingTypeIds { get; set; } = new List<int>();
        public List<int> BookingStatusIds { get; set; } = new List<int>();
        public List<int> CounselorIds { get; set; } = new List<int>();
        public string SearchAll { get; set; } = "";
    }
}
