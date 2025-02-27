
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
            public class PostCategoryService : IPostCategoryService
            {
                IPostCategoryRepository postCategoryRepository;
                public PostCategoryService(
                    IPostCategoryRepository _postCategoryRepository
                    )
                {
                    postCategoryRepository = _postCategoryRepository;
                }
                public async Task Add(PostCategory obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postCategoryRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postCategoryRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostCategory obj)
                {
                    obj.Active = 0;
                    await postCategoryRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postCategoryRepository.DeletePermanently(id);
                }
        
                public async Task<PostCategory> Detail(int? id)
                {
                    return await postCategoryRepository.Detail(id);
                }
        
                public async Task<List<PostCategory>> List()
                {
                    return await postCategoryRepository.List();
                }
        
                public async Task<List<PostCategory>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postCategoryRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostCategory>> ListServerSide(PostCategoryDTParameters parameters)
                {
                    return await postCategoryRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostCategory>> Search(string keyword)
                {
                    return await postCategoryRepository.Search(keyword);
                }
        
                public async Task Update(PostCategory obj)
                {
                    await postCategoryRepository.Update(obj);
                }
            }
        }
    
    