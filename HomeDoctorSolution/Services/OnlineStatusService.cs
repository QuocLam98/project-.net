
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
            public class OnlineStatusService : IOnlineStatusService
            {
                IOnlineStatusRepository onlineStatusRepository;
                public OnlineStatusService(
                    IOnlineStatusRepository _onlineStatusRepository
                    )
                {
                    onlineStatusRepository = _onlineStatusRepository;
                }
                public async Task Add(OnlineStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await onlineStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = onlineStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(OnlineStatus obj)
                {
                    obj.Active = 0;
                    await onlineStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await onlineStatusRepository.DeletePermanently(id);
                }
        
                public async Task<OnlineStatus> Detail(int? id)
                {
                    return await onlineStatusRepository.Detail(id);
                }
        
                public async Task<List<OnlineStatus>> List()
                {
                    return await onlineStatusRepository.List();
                }
        
                public async Task<List<OnlineStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await onlineStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OnlineStatusViewModel>> ListServerSide(OnlineStatusDTParameters parameters)
                {
                    return await onlineStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<OnlineStatus>> Search(string keyword)
                {
                    return await onlineStatusRepository.Search(keyword);
                }
        
                public async Task Update(OnlineStatus obj)
                {
                    await onlineStatusRepository.Update(obj);
                }
            }
        }
    
    