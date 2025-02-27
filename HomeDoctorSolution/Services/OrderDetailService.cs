
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        IOrderDetailRepository orderDetailRepository;
        public OrderDetailService(
            IOrderDetailRepository _orderDetailRepository
            )
        {
            orderDetailRepository = _orderDetailRepository;
        }
        public async Task Add(OrderDetail obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await orderDetailRepository.Add(obj);
        }

        public async Task<List<OrderDetail>> AddMany(List<OrderDetail> orderDetails)
        {
            await orderDetailRepository.AddMany(orderDetails);
            return orderDetails;
        }

        public int Count()
        {
            var result = orderDetailRepository.Count();
            return result;
        }

        public async Task Delete(OrderDetail obj)
        {
            obj.Active = 0;
            await orderDetailRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await orderDetailRepository.DeletePermanently(id);
        }

        public async Task<OrderDetail> Detail(int? id)
        {
            return await orderDetailRepository.Detail(id);
        }

        public async Task<List<OrderDetail>> List()
        {
            return await orderDetailRepository.List();
        }

        public async Task<List<OrderDetail>> ListPaging(int pageIndex, int pageSize)
        {
            return await orderDetailRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<OrderDetailViewModel>> ListServerSide(OrderDetailDTParameters parameters)
        {
            return await orderDetailRepository.ListServerSide(parameters);
        }

        public async Task<List<OrderDetail>> Search(string keyword)
        {
            return await orderDetailRepository.Search(keyword);
        }

        public async Task Update(OrderDetail obj)
        {
            await orderDetailRepository.Update(obj);
        }
    }
}

