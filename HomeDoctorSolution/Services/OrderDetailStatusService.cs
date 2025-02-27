
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
            public class OrderDetailStatusService : IOrderDetailStatusService
            {
                IOrderDetailStatusRepository orderDetailStatusRepository;
                public OrderDetailStatusService(
                    IOrderDetailStatusRepository _orderDetailStatusRepository
                    )
                {
                    orderDetailStatusRepository = _orderDetailStatusRepository;
                }
                public async Task Add(OrderDetailStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await orderDetailStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = orderDetailStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(OrderDetailStatus obj)
                {
                    obj.Active = 0;
                    await orderDetailStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await orderDetailStatusRepository.DeletePermanently(id);
                }
        
                public async Task<OrderDetailStatus> Detail(int? id)
                {
                    return await orderDetailStatusRepository.Detail(id);
                }
        
                public async Task<List<OrderDetailStatus>> List()
                {
                    return await orderDetailStatusRepository.List();
                }
        
                public async Task<List<OrderDetailStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await orderDetailStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OrderDetailStatus>> ListServerSide(OrderDetailStatusDTParameters parameters)
                {
                    return await orderDetailStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<OrderDetailStatus>> Search(string keyword)
                {
                    return await orderDetailStatusRepository.Search(keyword);
                }
        
                public async Task Update(OrderDetailStatus obj)
                {
                    await orderDetailStatusRepository.Update(obj);
                }
            }
        }
    
    