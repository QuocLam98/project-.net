
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
            public class SurveySectionQuestionService : ISurveySectionQuestionService
            {
                ISurveySectionQuestionRepository surveySectionQuestionRepository;
                public SurveySectionQuestionService(
                    ISurveySectionQuestionRepository _surveySectionQuestionRepository
                    )
                {
                    surveySectionQuestionRepository = _surveySectionQuestionRepository;
                }
                public async Task Add(SurveySectionQuestion obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveySectionQuestionRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveySectionQuestionRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveySectionQuestion obj)
                {
                    obj.Active = 0;
                    await surveySectionQuestionRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveySectionQuestionRepository.DeletePermanently(id);
                }
        
                public async Task<SurveySectionQuestion> Detail(int? id)
                {
                    return await surveySectionQuestionRepository.Detail(id);
                }
        
                public async Task<List<SurveySectionQuestion>> List()
                {
                    return await surveySectionQuestionRepository.List();
                }
        
                public async Task<List<SurveySectionQuestion>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveySectionQuestionRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveySectionQuestionViewModel>> ListServerSide(SurveySectionQuestionDTParameters parameters)
                {
                    return await surveySectionQuestionRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveySectionQuestion>> Search(string keyword)
                {
                    return await surveySectionQuestionRepository.Search(keyword);
                }
        
                public async Task Update(SurveySectionQuestion obj)
                {
                    await surveySectionQuestionRepository.Update(obj);
                }
            }
        }
    
    