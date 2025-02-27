
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
            public class PromotionService : IPromotionService
            {
                IPromotionRepository promotionRepository;
                public PromotionService(
                    IPromotionRepository _promotionRepository
                    )
                {
                    promotionRepository = _promotionRepository;
                }
                public async Task Add(Promotion obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await promotionRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = promotionRepository.Count();
                    return result;
                }
        
                public async Task Delete(Promotion obj)
                {
                    obj.Active = 0;
                    await promotionRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await promotionRepository.DeletePermanently(id);
                }
        
                public async Task<Promotion> Detail(int? id)
                {
                    return await promotionRepository.Detail(id);
                }
        
                public async Task<List<Promotion>> List()
                {
                    return await promotionRepository.List();
                }
        
                public async Task<List<Promotion>> ListPaging(int pageIndex, int pageSize)
                {
                    return await promotionRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<Promotion>> ListServerSide(PromotionDTParameters parameters)
                {
                    return await promotionRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Promotion>> Search(string keyword)
                {
                    return await promotionRepository.Search(keyword);
                }
        
                public async Task Update(Promotion obj)
                {
                    await promotionRepository.Update(obj);
                }
            }
        }
    
    