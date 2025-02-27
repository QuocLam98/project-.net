
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
            public class SubscribeService : ISubscribeService
            {
                ISubscribeRepository subscribeRepository;
                public SubscribeService(
                    ISubscribeRepository _subscribeRepository
                    )
                {
                    subscribeRepository = _subscribeRepository;
                }
                public async Task Add(Subscribe obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await subscribeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = subscribeRepository.Count();
                    return result;
                }
        
                public async Task Delete(Subscribe obj)
                {
                    obj.Active = 0;
                    await subscribeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await subscribeRepository.DeletePermanently(id);
                }
        
                public async Task<Subscribe> Detail(int? id)
                {
                    return await subscribeRepository.Detail(id);
                }
        
                public async Task<List<Subscribe>> List()
                {
                    return await subscribeRepository.List();
                }
        
                public async Task<List<Subscribe>> ListPaging(int pageIndex, int pageSize)
                {
                    return await subscribeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<Subscribe>> ListServerSide(SubscribeDTParameters parameters)
                {
                    return await subscribeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Subscribe>> Search(string keyword)
                {
                    return await subscribeRepository.Search(keyword);
                }
        
                public async Task Update(Subscribe obj)
                {
                    await subscribeRepository.Update(obj);
                }
            }
        }
    
    