
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class ProductDTParameters : DTParameters
    {
        public List<int> ProductCategoryIds { get; set; } = new List<int>();
        public List<int> ProductBrandIds { get; set; } = new List<int>();
        public List<int> ProductTypeIds { get; set; } = new List<int>();
        public List<int> ProductStatusIds { get; set; } = new List<int>();

        public string SearchAll { get; set; } = "";
    }
}
