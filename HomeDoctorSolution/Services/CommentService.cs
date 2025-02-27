
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
            public class CommentService : ICommentService
            {
                ICommentRepository commentRepository;
                public CommentService(
                    ICommentRepository _commentRepository
                    )
                {
                    commentRepository = _commentRepository;
                }
                public async Task Add(Comment obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await commentRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = commentRepository.Count();
                    return result;
                }
        
                public async Task Delete(Comment obj)
                {
                    obj.Active = 0;
                    await commentRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await commentRepository.DeletePermanently(id);
                }
        
                public async Task<Comment> Detail(int? id)
                {
                    return await commentRepository.Detail(id);
                }
        
                public async Task<List<Comment>> List()
                {
                    return await commentRepository.List();
                }
        
                public async Task<List<Comment>> ListPaging(int pageIndex, int pageSize)
                {
                    return await commentRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<CommentViewModel>> ListServerSide(CommentDTParameters parameters)
                {
                    return await commentRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Comment>> Search(string keyword)
                {
                    return await commentRepository.Search(keyword);
                }
        
                public async Task Update(Comment obj)
                {
                    await commentRepository.Update(obj);
                }
            }
        }
    
    