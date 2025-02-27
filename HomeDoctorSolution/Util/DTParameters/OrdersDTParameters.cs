
using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Util.Parameters
{
    public class OrdersDTParameters : DTParameters
    {
        public List<int> OrderTypeIds { get; set; } = new List<int>();
        public List<int> OrderStatusIds { get; set; } = new List<int>();
        public List<int> OrderPaymentStatusIds { get; set; } = new List<int>();
        public List<int> OrderStatusShipIds { get; set; } = new List<int>();
        public List<int> PromotionsIds { get; set; } = new List<int>();
        public List<int> AccountsIds { get; set; } = new List<int>();

        public string SearchAll { get; set; } = "";
    }
}
