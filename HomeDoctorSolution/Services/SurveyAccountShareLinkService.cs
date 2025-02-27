
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
            public class SurveyAccountShareLinkService : ISurveyAccountShareLinkService
            {
                ISurveyAccountShareLinkRepository surveyAccountShareLinkRepository;
                public SurveyAccountShareLinkService(
                    ISurveyAccountShareLinkRepository _surveyAccountShareLinkRepository
                    )
                {
                    surveyAccountShareLinkRepository = _surveyAccountShareLinkRepository;
                }
                public async Task Add(SurveyAccountShareLink obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveyAccountShareLinkRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveyAccountShareLinkRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveyAccountShareLink obj)
                {
                    obj.Active = 0;
                    await surveyAccountShareLinkRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveyAccountShareLinkRepository.DeletePermanently(id);
                }
        
                public async Task<SurveyAccountShareLink> Detail(int? id)
                {
                    return await surveyAccountShareLinkRepository.Detail(id);
                }
        
                public async Task<List<SurveyAccountShareLink>> List()
                {
                    return await surveyAccountShareLinkRepository.List();
                }
        
                public async Task<List<SurveyAccountShareLink>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveyAccountShareLinkRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveyAccountShareLink>> ListServerSide(SurveyAccountShareLinkDTParameters parameters)
                {
                    return await surveyAccountShareLinkRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveyAccountShareLink>> Search(string keyword)
                {
                    return await surveyAccountShareLinkRepository.Search(keyword);
                }
        
                public async Task Update(SurveyAccountShareLink obj)
                {
                    await surveyAccountShareLinkRepository.Update(obj);
                }
            }
        }
    
    