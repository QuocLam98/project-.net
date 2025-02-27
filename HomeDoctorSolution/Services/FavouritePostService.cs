
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
            public class FavouritePostService : IFavouritePostService
            {
                IFavouritePostRepository favouritePostRepository;
                public FavouritePostService(
                    IFavouritePostRepository _favouritePostRepository
                    )
                {
                    favouritePostRepository = _favouritePostRepository;
                }
                public async Task Add(FavouritePost obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await favouritePostRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = favouritePostRepository.Count();
                    return result;
                }
        
                public async Task Delete(FavouritePost obj)
                {
                    obj.Active = 0;
                    await favouritePostRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await favouritePostRepository.DeletePermanently(id);
                }
        
                public async Task<FavouritePost> Detail(int? id)
                {
                    return await favouritePostRepository.Detail(id);
                }
        
                public async Task<List<FavouritePost>> List()
                {
                    return await favouritePostRepository.List();
                }
        
                public async Task<List<FavouritePost>> ListPaging(int pageIndex, int pageSize)
                {
                    return await favouritePostRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<FavouritePostViewModel>> ListServerSide(FavouritePostDTParameters parameters)
                {
                    return await favouritePostRepository.ListServerSide(parameters);
                }
        
                public async Task<List<FavouritePost>> Search(string keyword)
                {
                    return await favouritePostRepository.Search(keyword);
                }
        
                public async Task Update(FavouritePost obj)
                {
                    await favouritePostRepository.Update(obj);
                }
            }
        }
    
    