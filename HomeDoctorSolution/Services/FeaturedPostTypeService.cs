
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
            public class FeaturedPostTypeService : IFeaturedPostTypeService
            {
                IFeaturedPostTypeRepository featuredPostTypeRepository;
                public FeaturedPostTypeService(
                    IFeaturedPostTypeRepository _featuredPostTypeRepository
                    )
                {
                    featuredPostTypeRepository = _featuredPostTypeRepository;
                }
                public async Task Add(FeaturedPostType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await featuredPostTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = featuredPostTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(FeaturedPostType obj)
                {
                    obj.Active = 0;
                    await featuredPostTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await featuredPostTypeRepository.DeletePermanently(id);
                }
        
                public async Task<FeaturedPostType> Detail(int? id)
                {
                    return await featuredPostTypeRepository.Detail(id);
                }
        
                public async Task<List<FeaturedPostType>> List()
                {
                    return await featuredPostTypeRepository.List();
                }
        
                public async Task<List<FeaturedPostType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await featuredPostTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<FeaturedPostType>> ListServerSide(FeaturedPostTypeDTParameters parameters)
                {
                    return await featuredPostTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<FeaturedPostType>> Search(string keyword)
                {
                    return await featuredPostTypeRepository.Search(keyword);
                }
        
                public async Task Update(FeaturedPostType obj)
                {
                    await featuredPostTypeRepository.Update(obj);
                }
            }
        }
    
    