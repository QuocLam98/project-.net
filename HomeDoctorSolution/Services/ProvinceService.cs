
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
            public class ProvinceService : IProvinceService
            {
                IProvinceRepository provinceRepository;
                public ProvinceService(
                    IProvinceRepository _provinceRepository
                    )
                {
                    provinceRepository = _provinceRepository;
                }
                public async Task Add(Province obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await provinceRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = provinceRepository.Count();
                    return result;
                }
        
                public async Task Delete(Province obj)
                {
                    obj.Active = 0;
                    await provinceRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await provinceRepository.DeletePermanently(id);
                }
        
                public async Task<Province> Detail(int? id)
                {
                    return await provinceRepository.Detail(id);
                }
        
                public async Task<List<Province>> List()
                {
                    return await provinceRepository.List();
                }
        
                public async Task<List<Province>> ListPaging(int pageIndex, int pageSize)
                {
                    return await provinceRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ProvinceViewModel>> ListServerSide(ProvinceDTParameters parameters)
                {
                    return await provinceRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Province>> Search(string keyword)
                {
                    return await provinceRepository.Search(keyword);
                }
        
                public async Task Update(Province obj)
                {
                    await provinceRepository.Update(obj);
                }
            }
        }
    
    