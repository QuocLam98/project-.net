
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
            public class AccountTypeService : IAccountTypeService
            {
                IAccountTypeRepository accountTypeRepository;
                public AccountTypeService(
                    IAccountTypeRepository _accountTypeRepository
                    )
                {
                    accountTypeRepository = _accountTypeRepository;
                }
                public async Task Add(AccountType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await accountTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = accountTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(AccountType obj)
                {
                    obj.Active = 0;
                    await accountTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await accountTypeRepository.DeletePermanently(id);
                }
        
                public async Task<AccountType> Detail(int? id)
                {
                    return await accountTypeRepository.Detail(id);
                }
        
                public async Task<List<AccountType>> List()
                {
                    return await accountTypeRepository.List();
                }
        
                public async Task<List<AccountType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await accountTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<AccountType>> ListServerSide(AccountTypeDTParameters parameters)
                {
                    return await accountTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<AccountType>> Search(string keyword)
                {
                    return await accountTypeRepository.Search(keyword);
                }
        
                public async Task Update(AccountType obj)
                {
                    await accountTypeRepository.Update(obj);
                }
            }
        }
    
    