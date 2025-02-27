
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class PostDTParameters : DTParameters
    {
        public List<int> PostTypeIds { get; set; } = new List<int>();
        public List<int> PostCategoryIds { get; set; } = new List<int>();
        public List<int> PostLayoutIds { get; set; } = new List<int>();
        public List<int> PostPublishStatusIds { get; set; } = new List<int>();
        public List<int> AuthorIds { get; set; } = new List<int>();

        public string SearchAll { get; set; } = "";
    }
}
