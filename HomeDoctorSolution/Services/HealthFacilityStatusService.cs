
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
            public class HealthFacilityStatusService : IHealthFacilityStatusService
            {
                IHealthFacilityStatusRepository healthFacilityStatusRepository;
                public HealthFacilityStatusService(
                    IHealthFacilityStatusRepository _healthFacilityStatusRepository
                    )
                {
                    healthFacilityStatusRepository = _healthFacilityStatusRepository;
                }
                public async Task Add(HealthFacilityStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await healthFacilityStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = healthFacilityStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(HealthFacilityStatus obj)
                {
                    obj.Active = 0;
                    await healthFacilityStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await healthFacilityStatusRepository.DeletePermanently(id);
                }
        
                public async Task<HealthFacilityStatus> Detail(int? id)
                {
                    return await healthFacilityStatusRepository.Detail(id);
                }
        
                public async Task<List<HealthFacilityStatus>> List()
                {
                    return await healthFacilityStatusRepository.List();
                }
        
                public async Task<List<HealthFacilityStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await healthFacilityStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<HealthFacilityStatus>> ListServerSide(HealthFacilityStatusDTParameters parameters)
                {
                    return await healthFacilityStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<HealthFacilityStatus>> Search(string keyword)
                {
                    return await healthFacilityStatusRepository.Search(keyword);
                }
        
                public async Task Update(HealthFacilityStatus obj)
                {
                    await healthFacilityStatusRepository.Update(obj);
                }
            }
        }
    
    