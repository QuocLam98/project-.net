
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
            public class PostLayoutService : IPostLayoutService
            {
                IPostLayoutRepository postLayoutRepository;
                public PostLayoutService(
                    IPostLayoutRepository _postLayoutRepository
                    )
                {
                    postLayoutRepository = _postLayoutRepository;
                }
                public async Task Add(PostLayout obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postLayoutRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postLayoutRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostLayout obj)
                {
                    obj.Active = 0;
                    await postLayoutRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postLayoutRepository.DeletePermanently(id);
                }
        
                public async Task<PostLayout> Detail(int? id)
                {
                    return await postLayoutRepository.Detail(id);
                }
        
                public async Task<List<PostLayout>> List()
                {
                    return await postLayoutRepository.List();
                }
        
                public async Task<List<PostLayout>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postLayoutRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostLayout>> ListServerSide(PostLayoutDTParameters parameters)
                {
                    return await postLayoutRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostLayout>> Search(string keyword)
                {
                    return await postLayoutRepository.Search(keyword);
                }
        
                public async Task Update(PostLayout obj)
                {
                    await postLayoutRepository.Update(obj);
                }
            }
        }
    
    