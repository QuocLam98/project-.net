
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
            public class FeaturedPostService : IFeaturedPostService
            {
                IFeaturedPostRepository featuredPostRepository;
                public FeaturedPostService(
                    IFeaturedPostRepository _featuredPostRepository
                    )
                {
                    featuredPostRepository = _featuredPostRepository;
                }
                public async Task Add(FeaturedPost obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await featuredPostRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = featuredPostRepository.Count();
                    return result;
                }
        
                public async Task Delete(FeaturedPost obj)
                {
                    obj.Active = 0;
                    await featuredPostRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await featuredPostRepository.DeletePermanently(id);
                }
        
                public async Task<FeaturedPost> Detail(int? id)
                {
                    return await featuredPostRepository.Detail(id);
                }
        
                public async Task<List<FeaturedPost>> List()
                {
                    return await featuredPostRepository.List();
                }
        
                public async Task<List<FeaturedPost>> ListPaging(int pageIndex, int pageSize)
                {
                    return await featuredPostRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<FeaturedPostViewModel>> ListServerSide(FeaturedPostDTParameters parameters)
                {
                    return await featuredPostRepository.ListServerSide(parameters);
                }
        
                public async Task<List<FeaturedPost>> Search(string keyword)
                {
                    return await featuredPostRepository.Search(keyword);
                }
        
                public async Task Update(FeaturedPost obj)
                {
                    await featuredPostRepository.Update(obj);
                }
            }
        }
    
    