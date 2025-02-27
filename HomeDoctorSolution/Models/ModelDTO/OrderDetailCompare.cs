using HomeDoctorSolution.Models;
using System;

namespace HomeDoctor.Models.ModelDTO
{
    public class OrderDetailCompare : IEqualityComparer<OrderDetail>
    {
        public bool Equals(OrderDetail x, OrderDetail y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(OrderDetail obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
