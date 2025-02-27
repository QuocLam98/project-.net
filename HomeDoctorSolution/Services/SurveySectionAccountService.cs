
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
            public class SurveySectionAccountService : ISurveySectionAccountService
            {
                ISurveySectionAccountRepository surveySectionAccountRepository;
                public SurveySectionAccountService(
                    ISurveySectionAccountRepository _surveySectionAccountRepository
                    )
                {
                    surveySectionAccountRepository = _surveySectionAccountRepository;
                }
                public async Task Add(SurveySectionAccount obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await surveySectionAccountRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = surveySectionAccountRepository.Count();
                    return result;
                }
        
                public async Task Delete(SurveySectionAccount obj)
                {
                    obj.Active = 0;
                    await surveySectionAccountRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await surveySectionAccountRepository.DeletePermanently(id);
                }
        
                public async Task<SurveySectionAccount> Detail(int? id)
                {
                    return await surveySectionAccountRepository.Detail(id);
                }
        
                public async Task<List<SurveySectionAccount>> List()
                {
                    return await surveySectionAccountRepository.List();
                }
        
                public async Task<List<SurveySectionAccount>> ListPaging(int pageIndex, int pageSize)
                {
                    return await surveySectionAccountRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<SurveySectionAccountViewModel>> ListServerSide(SurveySectionAccountDTParameters parameters)
                {
                    return await surveySectionAccountRepository.ListServerSide(parameters);
                }
        
                public async Task<List<SurveySectionAccount>> Search(string keyword)
                {
                    return await surveySectionAccountRepository.Search(keyword);
                }
        
                public async Task Update(SurveySectionAccount obj)
                {
                    await surveySectionAccountRepository.Update(obj);
                }
            }
        }
    
    