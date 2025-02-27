
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
            public class SurveyStatusService : ISurveyStatusService
            {
                ISurveyStatusRepository surveyStatusRepository;
                public SurveyStatusService(
                    ISurveyStatusRepository _surveyStatusRepository
                    )
                {
                    surveyStatusRepository = _surveyStatusRepository;
                }
                public async Task Add(SurveyStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveyStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveyStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveyStatus obj)
                {
                    obj.Active = 0;
                    await surveyStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveyStatusRepository.DeletePermanently(id);
                }
        
                public async Task<SurveyStatus> Detail(int? id)
                {
                    return await surveyStatusRepository.Detail(id);
                }
        
                public async Task<List<SurveyStatus>> List()
                {
                    return await surveyStatusRepository.List();
                }
        
                public async Task<List<SurveyStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveyStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveyStatus>> ListServerSide(SurveyStatusDTParameters parameters)
                {
                    return await surveyStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveyStatus>> Search(string keyword)
                {
                    return await surveyStatusRepository.Search(keyword);
                }
        
                public async Task Update(SurveyStatus obj)
                {
                    await surveyStatusRepository.Update(obj);
                }
            }
        }
    
    