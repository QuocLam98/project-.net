
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
            public class SystemConfigService : ISystemConfigService
            {
                ISystemConfigRepository systemConfigRepository;
                public SystemConfigService(
                    ISystemConfigRepository _systemConfigRepository
                    )
                {
                    systemConfigRepository = _systemConfigRepository;
                }
                public async Task Add(SystemConfig obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await systemConfigRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = systemConfigRepository.Count();
                    return result;
                }
        
                public async Task Delete(SystemConfig obj)
                {
                    obj.Active = 0;
                    await systemConfigRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await systemConfigRepository.DeletePermanently(id);
                }
        
                public async Task<SystemConfig> Detail(int? id)
                {
                    return await systemConfigRepository.Detail(id);
                }
        
                public async Task<List<SystemConfig>> List()
                {
                    return await systemConfigRepository.List();
                }
        
                public async Task<List<SystemConfig>> ListPaging(int pageIndex, int pageSize)
                {
                    return await systemConfigRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SystemConfig>> ListServerSide(SystemConfigDTParameters parameters)
                {
                    return await systemConfigRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SystemConfig>> Search(string keyword)
                {
                    return await systemConfigRepository.Search(keyword);
                }
        
                public async Task Update(SystemConfig obj)
                {
                    await systemConfigRepository.Update(obj);
                }
            }
        }
    
    