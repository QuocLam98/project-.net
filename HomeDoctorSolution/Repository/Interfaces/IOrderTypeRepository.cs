
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
        public interface IOrderTypeRepository
        {
            Task <List< OrderType>> List();

            Task <List< OrderType>> Search(string keyword);

            Task <List< OrderType>> ListPaging(int pageIndex, int pageSize);

            Task <OrderType> Detail(int ? postId);

            Task <OrderType> Add(OrderType OrderType);

            Task Update(OrderType OrderType);

            Task Delete(OrderType OrderType);

            Task <int> DeletePermanently(int ? OrderTypeId);

            int Count();

            Task <DTResult<OrderType>> ListServerSide(OrderTypeDTParameters parameters);
        }
    }
