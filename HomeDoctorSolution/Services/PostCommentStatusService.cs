
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
            public class PostCommentStatusService : IPostCommentStatusService
            {
                IPostCommentStatusRepository postCommentStatusRepository;
                public PostCommentStatusService(
                    IPostCommentStatusRepository _postCommentStatusRepository
                    )
                {
                    postCommentStatusRepository = _postCommentStatusRepository;
                }
                public async Task Add(PostCommentStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postCommentStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postCommentStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostCommentStatus obj)
                {
                    obj.Active = 0;
                    await postCommentStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postCommentStatusRepository.DeletePermanently(id);
                }
        
                public async Task<PostCommentStatus> Detail(int? id)
                {
                    return await postCommentStatusRepository.Detail(id);
                }
        
                public async Task<List<PostCommentStatus>> List()
                {
                    return await postCommentStatusRepository.List();
                }
        
                public async Task<List<PostCommentStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postCommentStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostCommentStatus>> ListServerSide(PostCommentStatusDTParameters parameters)
                {
                    return await postCommentStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostCommentStatus>> Search(string keyword)
                {
                    return await postCommentStatusRepository.Search(keyword);
                }
        
                public async Task Update(PostCommentStatus obj)
                {
                    await postCommentStatusRepository.Update(obj);
                }
            }
        }
    
    