
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
            public class HomepageContentTypeService : IHomepageContentTypeService
            {
                IHomepageContentTypeRepository homepageContentTypeRepository;
                public HomepageContentTypeService(
                    IHomepageContentTypeRepository _homepageContentTypeRepository
                    )
                {
                    homepageContentTypeRepository = _homepageContentTypeRepository;
                }
                public async Task Add(HomepageContentType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await homepageContentTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = homepageContentTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(HomepageContentType obj)
                {
                    obj.Active = 0;
                    await homepageContentTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await homepageContentTypeRepository.DeletePermanently(id);
                }
        
                public async Task<HomepageContentType> Detail(int? id)
                {
                    return await homepageContentTypeRepository.Detail(id);
                }
        
                public async Task<List<HomepageContentType>> List()
                {
                    return await homepageContentTypeRepository.List();
                }
        
                public async Task<List<HomepageContentType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await homepageContentTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<HomepageContentType>> ListServerSide(HomepageContentTypeDTParameters parameters)
                {
                    return await homepageContentTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<HomepageContentType>> Search(string keyword)
                {
                    return await homepageContentTypeRepository.Search(keyword);
                }
        
                public async Task Update(HomepageContentType obj)
                {
                    await homepageContentTypeRepository.Update(obj);
                }
            }
        }
    
    