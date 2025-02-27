
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
            public class SurveyMetaService : ISurveyMetaService
            {
                ISurveyMetaRepository surveyMetaRepository;
                public SurveyMetaService(
                    ISurveyMetaRepository _surveyMetaRepository
                    )
                {
                    surveyMetaRepository = _surveyMetaRepository;
                }
                public async Task Add(SurveyMeta obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveyMetaRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveyMetaRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveyMeta obj)
                {
                    obj.Active = 0;
                    await surveyMetaRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveyMetaRepository.DeletePermanently(id);
                }
        
                public async Task<SurveyMeta> Detail(int? id)
                {
                    return await surveyMetaRepository.Detail(id);
                }
        
                public async Task<List<SurveyMeta>> List()
                {
                    return await surveyMetaRepository.List();
                }
        
                public async Task<List<SurveyMeta>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveyMetaRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveyMetaViewModel>> ListServerSide(SurveyMetaDTParameters parameters)
                {
                    return await surveyMetaRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveyMeta>> Search(string keyword)
                {
                    return await surveyMetaRepository.Search(keyword);
                }
        
                public async Task Update(SurveyMeta obj)
                {
                    await surveyMetaRepository.Update(obj);
                }
            }
        }
    
    