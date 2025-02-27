
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
            public class OrderPaymentStatusService : IOrderPaymentStatusService
            {
                IOrderPaymentStatusRepository orderPaymentStatusRepository;
                public OrderPaymentStatusService(
                    IOrderPaymentStatusRepository _orderPaymentStatusRepository
                    )
                {
                    orderPaymentStatusRepository = _orderPaymentStatusRepository;
                }
                public async Task Add(OrderPaymentStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await orderPaymentStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = orderPaymentStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(OrderPaymentStatus obj)
                {
                    obj.Active = 0;
                    await orderPaymentStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await orderPaymentStatusRepository.DeletePermanently(id);
                }
        
                public async Task<OrderPaymentStatus> Detail(int? id)
                {
                    return await orderPaymentStatusRepository.Detail(id);
                }
        
                public async Task<List<OrderPaymentStatus>> List()
                {
                    return await orderPaymentStatusRepository.List();
                }
        
                public async Task<List<OrderPaymentStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await orderPaymentStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OrderPaymentStatus>> ListServerSide(OrderPaymentStatusDTParameters parameters)
                {
                    return await orderPaymentStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<OrderPaymentStatus>> Search(string keyword)
                {
                    return await orderPaymentStatusRepository.Search(keyword);
                }
        
                public async Task Update(OrderPaymentStatus obj)
                {
                    await orderPaymentStatusRepository.Update(obj);
                }
            }
        }
    
    