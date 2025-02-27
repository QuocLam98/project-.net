
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
            public class HomepageContentService : IHomepageContentService
            {
                IHomepageContentRepository homepageContentRepository;
                public HomepageContentService(
                    IHomepageContentRepository _homepageContentRepository
                    )
                {
                    homepageContentRepository = _homepageContentRepository;
                }
                public async Task Add(HomepageContent obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await homepageContentRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = homepageContentRepository.Count();
                    return result;
                }
        
                public async Task Delete(HomepageContent obj)
                {
                    obj.Active = 0;
                    await homepageContentRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await homepageContentRepository.DeletePermanently(id);
                }
        
                public async Task<HomepageContent> Detail(int? id)
                {
                    return await homepageContentRepository.Detail(id);
                }
        
                public async Task<List<HomepageContent>> List()
                {
                    return await homepageContentRepository.List();
                }
        
                public async Task<List<HomepageContent>> ListPaging(int pageIndex, int pageSize)
                {
                    return await homepageContentRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<HomepageContentViewModel>> ListServerSide(HomepageContentDTParameters parameters)
                {
                    return await homepageContentRepository.ListServerSide(parameters);
                }
        
                public async Task<List<HomepageContent>> Search(string keyword)
                {
                    return await homepageContentRepository.Search(keyword);
                }
        
                public async Task Update(HomepageContent obj)
                {
                    await homepageContentRepository.Update(obj);
                }
            }
        }
    
    