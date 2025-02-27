
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
            public class OrderStatusShipService : IOrderStatusShipService
            {
                IOrderStatusShipRepository orderStatusShipRepository;
                public OrderStatusShipService(
                    IOrderStatusShipRepository _orderStatusShipRepository
                    )
                {
                    orderStatusShipRepository = _orderStatusShipRepository;
                }
                public async Task Add(OrderStatusShip obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await orderStatusShipRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = orderStatusShipRepository.Count();
                    return result;
                }
        
                public async Task Delete(OrderStatusShip obj)
                {
                    obj.Active = 0;
                    await orderStatusShipRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await orderStatusShipRepository.DeletePermanently(id);
                }
        
                public async Task<OrderStatusShip> Detail(int? id)
                {
                    return await orderStatusShipRepository.Detail(id);
                }
        
                public async Task<List<OrderStatusShip>> List()
                {
                    return await orderStatusShipRepository.List();
                }
        
                public async Task<List<OrderStatusShip>> ListPaging(int pageIndex, int pageSize)
                {
                    return await orderStatusShipRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OrderStatusShip>> ListServerSide(OrderStatusShipDTParameters parameters)
                {
                    return await orderStatusShipRepository.ListServerSide(parameters);
                }
        
                public async Task<List<OrderStatusShip>> Search(string keyword)
                {
                    return await orderStatusShipRepository.Search(keyword);
                }
        
                public async Task Update(OrderStatusShip obj)
                {
                    await orderStatusShipRepository.Update(obj);
                }
            }
        }
    
    