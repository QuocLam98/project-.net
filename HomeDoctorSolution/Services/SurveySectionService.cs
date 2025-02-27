
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
            public class SurveySectionService : ISurveySectionService
            {
                ISurveySectionRepository surveySectionRepository;
                public SurveySectionService(
                    ISurveySectionRepository _surveySectionRepository
                    )
                {
                    surveySectionRepository = _surveySectionRepository;
                }
                public async Task Add(SurveySection obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveySectionRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveySectionRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveySection obj)
                {
                    obj.Active = 0;
                    await surveySectionRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveySectionRepository.DeletePermanently(id);
                }
        
                public async Task<SurveySection> Detail(int? id)
                {
                    return await surveySectionRepository.Detail(id);
                }
        
                public async Task<List<SurveySection>> List()
                {
                    return await surveySectionRepository.List();
                }
        
                public async Task<List<SurveySection>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveySectionRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveySectionViewModel>> ListServerSide(SurveySectionDTParameters parameters)
                {
                    return await surveySectionRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveySection>> Search(string keyword)
                {
                    return await surveySectionRepository.Search(keyword);
                }
        
                public async Task Update(SurveySection obj)
                {
                    await surveySectionRepository.Update(obj);
                }
            }
        }
    
    