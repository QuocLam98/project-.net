
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
            public class PostTagService : IPostTagService
            {
                IPostTagRepository postTagRepository;
                public PostTagService(
                    IPostTagRepository _postTagRepository
                    )
                {
                    postTagRepository = _postTagRepository;
                }
                public async Task Add(PostTag obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postTagRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postTagRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostTag obj)
                {
                    obj.Active = 0;
                    await postTagRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postTagRepository.DeletePermanently(id);
                }
        
                public async Task<PostTag> Detail(int? id)
                {
                    return await postTagRepository.Detail(id);
                }
        
                public async Task<List<PostTag>> List()
                {
                    return await postTagRepository.List();
                }
        
                public async Task<List<PostTag>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postTagRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostTagViewModel>> ListServerSide(PostTagDTParameters parameters)
                {
                    return await postTagRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostTag>> Search(string keyword)
                {
                    return await postTagRepository.Search(keyword);
                }
        
                public async Task Update(PostTag obj)
                {
                    await postTagRepository.Update(obj);
                }
            }
        }
    
    