
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
            public class CommentStatusService : ICommentStatusService
            {
                ICommentStatusRepository commentStatusRepository;
                public CommentStatusService(
                    ICommentStatusRepository _commentStatusRepository
                    )
                {
                    commentStatusRepository = _commentStatusRepository;
                }
                public async Task Add(CommentStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await commentStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = commentStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(CommentStatus obj)
                {
                    obj.Active = 0;
                    await commentStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await commentStatusRepository.DeletePermanently(id);
                }
        
                public async Task<CommentStatus> Detail(int? id)
                {
                    return await commentStatusRepository.Detail(id);
                }
        
                public async Task<List<CommentStatus>> List()
                {
                    return await commentStatusRepository.List();
                }
        
                public async Task<List<CommentStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await commentStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<CommentStatus>> ListServerSide(CommentStatusDTParameters parameters)
                {
                    return await commentStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<CommentStatus>> Search(string keyword)
                {
                    return await commentStatusRepository.Search(keyword);
                }
        
                public async Task Update(CommentStatus obj)
                {
                    await commentStatusRepository.Update(obj);
                }
            }
        }
    
    