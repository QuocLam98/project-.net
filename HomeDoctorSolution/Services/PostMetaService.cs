
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
            public class PostMetaService : IPostMetaService
            {
                IPostMetaRepository postMetaRepository;
                public PostMetaService(
                    IPostMetaRepository _postMetaRepository
                    )
                {
                    postMetaRepository = _postMetaRepository;
                }
                public async Task Add(PostMeta obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postMetaRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postMetaRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostMeta obj)
                {
                    obj.Active = 0;
                    await postMetaRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postMetaRepository.DeletePermanently(id);
                }
        
                public async Task<PostMeta> Detail(int? id)
                {
                    return await postMetaRepository.Detail(id);
                }
        
                public async Task<List<PostMeta>> List()
                {
                    return await postMetaRepository.List();
                }
        
                public async Task<List<PostMeta>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postMetaRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostMetaViewModel>> ListServerSide(PostMetaDTParameters parameters)
                {
                    return await postMetaRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostMeta>> Search(string keyword)
                {
                    return await postMetaRepository.Search(keyword);
                }
        
                public async Task Update(PostMeta obj)
                {
                    await postMetaRepository.Update(obj);
                }
            }
        }
    
    