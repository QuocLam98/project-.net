
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
            public class HealthFacilityTypeService : IHealthFacilityTypeService
            {
                IHealthFacilityTypeRepository healthFacilityTypeRepository;
                public HealthFacilityTypeService(
                    IHealthFacilityTypeRepository _healthFacilityTypeRepository
                    )
                {
                    healthFacilityTypeRepository = _healthFacilityTypeRepository;
                }
                public async Task Add(HealthFacilityType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await healthFacilityTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = healthFacilityTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(HealthFacilityType obj)
                {
                    obj.Active = 0;
                    await healthFacilityTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await healthFacilityTypeRepository.DeletePermanently(id);
                }
        
                public async Task<HealthFacilityType> Detail(int? id)
                {
                    return await healthFacilityTypeRepository.Detail(id);
                }
        
                public async Task<List<HealthFacilityType>> List()
                {
                    return await healthFacilityTypeRepository.List();
                }
        
                public async Task<List<HealthFacilityType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await healthFacilityTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<HealthFacilityType>> ListServerSide(HealthFacilityTypeDTParameters parameters)
                {
                    return await healthFacilityTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<HealthFacilityType>> Search(string keyword)
                {
                    return await healthFacilityTypeRepository.Search(keyword);
                }
        
                public async Task Update(HealthFacilityType obj)
                {
                    await healthFacilityTypeRepository.Update(obj);
                }
            }
        }
    
    