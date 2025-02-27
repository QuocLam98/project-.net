
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
        public interface IOrderStatusShipRepository
        {
            Task <List< OrderStatusShip>> List();

            Task <List< OrderStatusShip>> Search(string keyword);

            Task <List< OrderStatusShip>> ListPaging(int pageIndex, int pageSize);

            Task <OrderStatusShip> Detail(int ? postId);

            Task <OrderStatusShip> Add(OrderStatusShip OrderStatusShip);

            Task Update(OrderStatusShip OrderStatusShip);

            Task Delete(OrderStatusShip OrderStatusShip);

            Task <int> DeletePermanently(int ? OrderStatusShipId);

            int Count();

            Task <DTResult<OrderStatusShip>> ListServerSide(OrderStatusShipDTParameters parameters);
        }
    }
