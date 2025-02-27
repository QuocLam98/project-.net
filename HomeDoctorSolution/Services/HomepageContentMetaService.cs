
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
            public class HomepageContentMetaService : IHomepageContentMetaService
            {
                IHomepageContentMetaRepository homepageContentMetaRepository;
                public HomepageContentMetaService(
                    IHomepageContentMetaRepository _homepageContentMetaRepository
                    )
                {
                    homepageContentMetaRepository = _homepageContentMetaRepository;
                }
                public async Task Add(HomepageContentMeta obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await homepageContentMetaRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = homepageContentMetaRepository.Count();
                    return result;
                }
        
                public async Task Delete(HomepageContentMeta obj)
                {
                    obj.Active = 0;
                    await homepageContentMetaRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await homepageContentMetaRepository.DeletePermanently(id);
                }
        
                public async Task<HomepageContentMeta> Detail(int? id)
                {
                    return await homepageContentMetaRepository.Detail(id);
                }
        
                public async Task<List<HomepageContentMeta>> List()
                {
                    return await homepageContentMetaRepository.List();
                }
        
                public async Task<List<HomepageContentMeta>> ListPaging(int pageIndex, int pageSize)
                {
                    return await homepageContentMetaRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<HomepageContentMetaViewModel>> ListServerSide(HomepageContentMetaDTParameters parameters)
                {
                    return await homepageContentMetaRepository.ListServerSide(parameters);
                }
        
                public async Task<List<HomepageContentMeta>> Search(string keyword)
                {
                    return await homepageContentMetaRepository.Search(keyword);
                }
        
                public async Task Update(HomepageContentMeta obj)
                {
                    await homepageContentMetaRepository.Update(obj);
                }
            }
        }
    
    