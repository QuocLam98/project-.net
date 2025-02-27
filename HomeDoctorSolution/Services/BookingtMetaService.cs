
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
            public class BookingtMetaService : IBookingtMetaService
            {
                IBookingtMetaRepository bookingtMetaRepository;
                public BookingtMetaService(
                    IBookingtMetaRepository _bookingtMetaRepository
                    )
                {
                    bookingtMetaRepository = _bookingtMetaRepository;
                }
                public async Task Add(BookingtMeta obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await bookingtMetaRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = bookingtMetaRepository.Count();
                    return result;
                }
        
                public async Task Delete(BookingtMeta obj)
                {
                    obj.Active = 0;
                    await bookingtMetaRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await bookingtMetaRepository.DeletePermanently(id);
                }
        
                public async Task<BookingtMeta> Detail(int? id)
                {
                    return await bookingtMetaRepository.Detail(id);
                }
        
                public async Task<List<BookingtMeta>> List()
                {
                    return await bookingtMetaRepository.List();
                }
        
                public async Task<List<BookingtMeta>> ListPaging(int pageIndex, int pageSize)
                {
                    return await bookingtMetaRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<BookingtMetaViewModel>> ListServerSide(BookingtMetaDTParameters parameters)
                {
                    return await bookingtMetaRepository.ListServerSide(parameters);
                }
        
                public async Task<List<BookingtMeta>> Search(string keyword)
                {
                    return await bookingtMetaRepository.Search(keyword);
                }
        
                public async Task Update(BookingtMeta obj)
                {
                    await bookingtMetaRepository.Update(obj);
                }
            }
        }
    
    