
    using HomeDoctorSolution.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HomeDoctorSolution.Util;
    using HomeDoctorSolution.Util.Parameters;
    using HomeDoctorSolution.Models.ViewModels;


    namespace HomeDoctorSolution.Repository
    {
        public interface IOrderStatusRepository
        {
            Task <List< OrderStatus>> List();

            Task <List< OrderStatus>> Search(string keyword);

            Task <List< OrderStatus>> ListPaging(int pageIndex, int pageSize);

            Task <OrderStatus> Detail(int ? postId);

            Task <OrderStatus> Add(OrderStatus OrderStatus);

            Task Update(OrderStatus OrderStatus);

            Task Delete(OrderStatus OrderStatus);

            Task <int> DeletePermanently(int ? OrderStatusId);

            int Count();

            Task <DTResult<OrderStatus>> ListServerSide(OrderStatusDTParameters parameters);
        }
    }
