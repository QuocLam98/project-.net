
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
            public class SurveySectionAccountDetailService : ISurveySectionAccountDetailService
            {
                ISurveySectionAccountDetailRepository surveySectionAccountDetailRepository;
                public SurveySectionAccountDetailService(
                    ISurveySectionAccountDetailRepository _surveySectionAccountDetailRepository
                    )
                {
                    surveySectionAccountDetailRepository = _surveySectionAccountDetailRepository;
                }
                public async Task Add(SurveySectionAccountDetail obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveySectionAccountDetailRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveySectionAccountDetailRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveySectionAccountDetail obj)
                {
                    obj.Active = 0;
                    await surveySectionAccountDetailRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveySectionAccountDetailRepository.DeletePermanently(id);
                }
        
                public async Task<SurveySectionAccountDetail> Detail(int? id)
                {
                    return await surveySectionAccountDetailRepository.Detail(id);
                }
        
                public async Task<List<SurveySectionAccountDetail>> List()
                {
                    return await surveySectionAccountDetailRepository.List();
                }
        
                public async Task<List<SurveySectionAccountDetail>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveySectionAccountDetailRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveySectionAccountDetailViewModel>> ListServerSide(SurveySectionAccountDetailDTParameters parameters)
                {
                    return await surveySectionAccountDetailRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveySectionAccountDetail>> Search(string keyword)
                {
                    return await surveySectionAccountDetailRepository.Search(keyword);
                }
        
                public async Task Update(SurveySectionAccountDetail obj)
                {
                    await surveySectionAccountDetailRepository.Update(obj);
                }
            }
        }
    
    