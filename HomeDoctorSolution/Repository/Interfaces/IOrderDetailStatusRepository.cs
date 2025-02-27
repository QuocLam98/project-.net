
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
        public interface IOrderDetailStatusRepository
        {
            Task <List< OrderDetailStatus>> List();

            Task <List< OrderDetailStatus>> Search(string keyword);

            Task <List< OrderDetailStatus>> ListPaging(int pageIndex, int pageSize);

            Task <OrderDetailStatus> Detail(int ? postId);

            Task <OrderDetailStatus> Add(OrderDetailStatus OrderDetailStatus);

            Task Update(OrderDetailStatus OrderDetailStatus);

            Task Delete(OrderDetailStatus OrderDetailStatus);

            Task <int> DeletePermanently(int ? OrderDetailStatusId);

            int Count();

            Task <DTResult<OrderDetailStatus>> ListServerSide(OrderDetailStatusDTParameters parameters);
        }
    }
