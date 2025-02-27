
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
            public class PostPublishStatusService : IPostPublishStatusService
            {
                IPostPublishStatusRepository postPublishStatusRepository;
                public PostPublishStatusService(
                    IPostPublishStatusRepository _postPublishStatusRepository
                    )
                {
                    postPublishStatusRepository = _postPublishStatusRepository;
                }
                public async Task Add(PostPublishStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postPublishStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postPublishStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostPublishStatus obj)
                {
                    obj.Active = 0;
                    await postPublishStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postPublishStatusRepository.DeletePermanently(id);
                }
        
                public async Task<PostPublishStatus> Detail(int? id)
                {
                    return await postPublishStatusRepository.Detail(id);
                }
        
                public async Task<List<PostPublishStatus>> List()
                {
                    return await postPublishStatusRepository.List();
                }
        
                public async Task<List<PostPublishStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postPublishStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostPublishStatus>> ListServerSide(PostPublishStatusDTParameters parameters)
                {
                    return await postPublishStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostPublishStatus>> Search(string keyword)
                {
                    return await postPublishStatusRepository.Search(keyword);
                }
        
                public async Task Update(PostPublishStatus obj)
                {
                    await postPublishStatusRepository.Update(obj);
                }
            }
        }
    
    