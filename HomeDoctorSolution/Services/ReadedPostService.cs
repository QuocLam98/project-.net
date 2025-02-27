
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
            public class ReadedPostService : IReadedPostService
            {
                IReadedPostRepository readedPostRepository;
                public ReadedPostService(
                    IReadedPostRepository _readedPostRepository
                    )
                {
                    readedPostRepository = _readedPostRepository;
                }
                public async Task Add(ReadedPost obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await readedPostRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = readedPostRepository.Count();
                    return result;
                }
        
                public async Task Delete(ReadedPost obj)
                {
                    obj.Active = 0;
                    await readedPostRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await readedPostRepository.DeletePermanently(id);
                }
        
                public async Task<ReadedPost> Detail(int? id)
                {
                    return await readedPostRepository.Detail(id);
                }
        
                public async Task<List<ReadedPost>> List()
                {
                    return await readedPostRepository.List();
                }
        
                public async Task<List<ReadedPost>> ListPaging(int pageIndex, int pageSize)
                {
                    return await readedPostRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ReadedPostViewModel>> ListServerSide(ReadedPostDTParameters parameters)
                {
                    return await readedPostRepository.ListServerSide(parameters);
                }
        
                public async Task<List<ReadedPost>> Search(string keyword)
                {
                    return await readedPostRepository.Search(keyword);
                }
        
                public async Task Update(ReadedPost obj)
                {
                    await readedPostRepository.Update(obj);
                }
            }
        }
    
    