
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
            public class AccountMetaService : IAccountMetaService
            {
                IAccountMetaRepository accountMetaRepository;
                public AccountMetaService(
                    IAccountMetaRepository _accountMetaRepository
                    )
                {
                    accountMetaRepository = _accountMetaRepository;
                }
                public async Task Add(AccountMeta obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await accountMetaRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = accountMetaRepository.Count();
                    return result;
                }
        
                public async Task Delete(AccountMeta obj)
                {
                    obj.Active = 0;
                    await accountMetaRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await accountMetaRepository.DeletePermanently(id);
                }
        
                public async Task<AccountMeta> Detail(int? id)
                {
                    return await accountMetaRepository.Detail(id);
                }
        
                public async Task<List<AccountMeta>> List()
                {
                    return await accountMetaRepository.List();
                }
        
                public async Task<List<AccountMeta>> ListPaging(int pageIndex, int pageSize)
                {
                    return await accountMetaRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<AccountMetaViewModel>> ListServerSide(AccountMetaDTParameters parameters)
                {
                    return await accountMetaRepository.ListServerSide(parameters);
                }
        
                public async Task<List<AccountMeta>> Search(string keyword)
                {
                    return await accountMetaRepository.Search(keyword);
                }
        
                public async Task Update(AccountMeta obj)
                {
                    await accountMetaRepository.Update(obj);
                }
            }
        }
    
    