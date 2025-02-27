
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
            public class DoctorTypeService : IDoctorTypeService
            {
                IDoctorTypeRepository doctorTypeRepository;
                public DoctorTypeService(
                    IDoctorTypeRepository _doctorTypeRepository
                    )
                {
                    doctorTypeRepository = _doctorTypeRepository;
                }
                public async Task Add(DoctorType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await doctorTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = doctorTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(DoctorType obj)
                {
                    obj.Active = 0;
                    await doctorTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await doctorTypeRepository.DeletePermanently(id);
                }
        
                public async Task<DoctorType> Detail(int? id)
                {
                    return await doctorTypeRepository.Detail(id);
                }
        
                public async Task<List<DoctorType>> List()
                {
                    return await doctorTypeRepository.List();
                }
        
                public async Task<List<DoctorType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await doctorTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<DoctorType>> ListServerSide(DoctorTypeDTParameters parameters)
                {
                    return await doctorTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<DoctorType>> Search(string keyword)
                {
                    return await doctorTypeRepository.Search(keyword);
                }
        
                public async Task Update(DoctorType obj)
                {
                    await doctorTypeRepository.Update(obj);
                }
            }
        }
    
    