
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class ClassDTParameters : DTParameters
    {
        public List<int> SchoolIds { get; set; } = new List<int>();
        public List<int> GradeIds { get; set; } = new List<int>();
        public List<int> SchoolYearIds { get; set; } = new List<int>();
        public string SearchAll { get; set; } = "";
    }
}
