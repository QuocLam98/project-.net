
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
            public class SurveyAccountService : ISurveyAccountService
            {
                ISurveyAccountRepository surveyAccountRepository;
                public SurveyAccountService(
                    ISurveyAccountRepository _surveyAccountRepository
                    )
                {
                    surveyAccountRepository = _surveyAccountRepository;
                }
                public async Task Add(SurveyAccount obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveyAccountRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveyAccountRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveyAccount obj)
                {
                    obj.Active = 0;
                    await surveyAccountRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveyAccountRepository.DeletePermanently(id);
                }
        
                public async Task<SurveyAccount> Detail(int? id)
                {
                    return await surveyAccountRepository.Detail(id);
                }
        
                public async Task<List<SurveyAccount>> List()
                {
                    return await surveyAccountRepository.List();
                }
        
                public async Task<List<SurveyAccount>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveyAccountRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveyAccountViewModel>> ListServerSide(SurveyAccountDTParameters parameters)
                {
                    return await surveyAccountRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveyAccount>> Search(string keyword)
                {
                    return await surveyAccountRepository.Search(keyword);
                }
        
                public async Task Update(SurveyAccount obj)
                {
                    await surveyAccountRepository.Update(obj);
                }
            }
        }
    
    