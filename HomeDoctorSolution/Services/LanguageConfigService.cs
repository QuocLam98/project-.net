
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
            public class LanguageConfigService : ILanguageConfigService
            {
                ILanguageConfigRepository languageConfigRepository;
                public LanguageConfigService(
                    ILanguageConfigRepository _languageConfigRepository
                    )
                {
                    languageConfigRepository = _languageConfigRepository;
                }
                public async Task Add(LanguageConfig obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await languageConfigRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = languageConfigRepository.Count();
                    return result;
                }
        
                public async Task Delete(LanguageConfig obj)
                {
                    obj.Active = 0;
                    await languageConfigRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await languageConfigRepository.DeletePermanently(id);
                }
        
                public async Task<LanguageConfig> Detail(int? id)
                {
                    return await languageConfigRepository.Detail(id);
                }
        
                public async Task<List<LanguageConfig>> List()
                {
                    return await languageConfigRepository.List();
                }
        
                public async Task<List<LanguageConfig>> ListPaging(int pageIndex, int pageSize)
                {
                    return await languageConfigRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<LanguageConfig>> ListServerSide(LanguageConfigDTParameters parameters)
                {
                    return await languageConfigRepository.ListServerSide(parameters);
                }
        
                public async Task<List<LanguageConfig>> Search(string keyword)
                {
                    return await languageConfigRepository.Search(keyword);
                }
        
                public async Task Update(LanguageConfig obj)
                {
                    await languageConfigRepository.Update(obj);
                }
            }
        }
    
    