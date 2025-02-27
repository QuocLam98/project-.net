
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
            public class SurveyService : ISurveyService
            {
                ISurveyRepository surveyRepository;
                public SurveyService(
                    ISurveyRepository _surveyRepository
                    )
                {
                    surveyRepository = _surveyRepository;
                }
                public async Task Add(Survey obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveyRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveyRepository.Count();
                    return result;
                }
        
                public async Task Delete(Survey obj)
                {
                    obj.Active = 0;
                    await surveyRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveyRepository.DeletePermanently(id);
                }
        
                public async Task<Survey> Detail(int? id)
                {
                    return await surveyRepository.Detail(id);
                }
        
                public async Task<List<Survey>> List()
                {
                    return await surveyRepository.List();
                }
        
                public async Task<List<Survey>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveyRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveyViewModel>> ListServerSide(SurveyDTParameters parameters)
                {
                    return await surveyRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Survey>> Search(string keyword)
                {
                    return await surveyRepository.Search(keyword);
                }
        
                public async Task Update(Survey obj)
                {
                    await surveyRepository.Update(obj);
                }
            }
        }
    
    