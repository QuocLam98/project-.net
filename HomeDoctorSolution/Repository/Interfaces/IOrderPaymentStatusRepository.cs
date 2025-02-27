
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
        public interface IOrderPaymentStatusRepository
        {
            Task <List< OrderPaymentStatus>> List();

            Task <List< OrderPaymentStatus>> Search(string keyword);

            Task <List< OrderPaymentStatus>> ListPaging(int pageIndex, int pageSize);

            Task <OrderPaymentStatus> Detail(int ? postId);

            Task <OrderPaymentStatus> Add(OrderPaymentStatus OrderPaymentStatus);

            Task Update(OrderPaymentStatus OrderPaymentStatus);

            Task Delete(OrderPaymentStatus OrderPaymentStatus);

            Task <int> DeletePermanently(int ? OrderPaymentStatusId);

            int Count();

            Task <DTResult<OrderPaymentStatus>> ListServerSide(OrderPaymentStatusDTParameters parameters);
        }
    }
