
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
            public class PostTypeService : IPostTypeService
            {
                IPostTypeRepository postTypeRepository;
                public PostTypeService(
                    IPostTypeRepository _postTypeRepository
                    )
                {
                    postTypeRepository = _postTypeRepository;
                }
                public async Task Add(PostType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await postTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = postTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(PostType obj)
                {
                    obj.Active = 0;
                    await postTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await postTypeRepository.DeletePermanently(id);
                }
        
                public async Task<PostType> Detail(int? id)
                {
                    return await postTypeRepository.Detail(id);
                }
        
                public async Task<List<PostType>> List()
                {
                    return await postTypeRepository.List();
                }
        
                public async Task<List<PostType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await postTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<PostType>> ListServerSide(PostTypeDTParameters parameters)
                {
                    return await postTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<PostType>> Search(string keyword)
                {
                    return await postTypeRepository.Search(keyword);
                }
        
                public async Task Update(PostType obj)
                {
                    await postTypeRepository.Update(obj);
                }
            }
        }
    
    