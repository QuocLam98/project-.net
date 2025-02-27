
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
            public class SurveyTypeService : ISurveyTypeService
            {
                ISurveyTypeRepository surveyTypeRepository;
                public SurveyTypeService(
                    ISurveyTypeRepository _surveyTypeRepository
                    )
                {
                    surveyTypeRepository = _surveyTypeRepository;
                }
                public async Task Add(SurveyType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveyTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveyTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveyType obj)
                {
                    obj.Active = 0;
                    await surveyTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveyTypeRepository.DeletePermanently(id);
                }
        
                public async Task<SurveyType> Detail(int? id)
                {
                    return await surveyTypeRepository.Detail(id);
                }
        
                public async Task<List<SurveyType>> List()
                {
                    return await surveyTypeRepository.List();
                }
        
                public async Task<List<SurveyType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveyTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveyType>> ListServerSide(SurveyTypeDTParameters parameters)
                {
                    return await surveyTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveyType>> Search(string keyword)
                {
                    return await surveyTypeRepository.Search(keyword);
                }
        
                public async Task Update(SurveyType obj)
                {
                    await surveyTypeRepository.Update(obj);
                }
            }
        }
    
    