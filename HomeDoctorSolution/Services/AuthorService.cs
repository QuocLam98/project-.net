
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
            public class AuthorService : IAuthorService
            {
                IAuthorRepository authorRepository;
                public AuthorService(
                    IAuthorRepository _authorRepository
                    )
                {
                    authorRepository = _authorRepository;
                }
                public async Task Add(Author obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await authorRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = authorRepository.Count();
                    return result;
                }
        
                public async Task Delete(Author obj)
                {
                    obj.Active = 0;
                    await authorRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await authorRepository.DeletePermanently(id);
                }
        
                public async Task<Author> Detail(int? id)
                {
                    return await authorRepository.Detail(id);
                }
        
                public async Task<List<Author>> List()
                {
                    return await authorRepository.List();
                }
        
                public async Task<List<Author>> ListPaging(int pageIndex, int pageSize)
                {
                    return await authorRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<AuthorViewModel>> ListServerSide(AuthorDTParameters parameters)
                {
                    return await authorRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Author>> Search(string keyword)
                {
                    return await authorRepository.Search(keyword);
                }
        
                public async Task Update(Author obj)
                {
                    await authorRepository.Update(obj);
                }
            }
        }
    
    