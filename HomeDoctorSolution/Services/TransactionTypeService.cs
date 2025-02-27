
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
            public class TransactionTypeService : ITransactionTypeService
            {
                ITransactionTypeRepository transactionTypeRepository;
                public TransactionTypeService(
                    ITransactionTypeRepository _transactionTypeRepository
                    )
                {
                    transactionTypeRepository = _transactionTypeRepository;
                }
                public async Task Add(TransactionType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await transactionTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = transactionTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(TransactionType obj)
                {
                    obj.Active = 0;
                    await transactionTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await transactionTypeRepository.DeletePermanently(id);
                }
        
                public async Task<TransactionType> Detail(int? id)
                {
                    return await transactionTypeRepository.Detail(id);
                }
        
                public async Task<List<TransactionType>> List()
                {
                    return await transactionTypeRepository.List();
                }
        
                public async Task<List<TransactionType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await transactionTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<TransactionType>> ListServerSide(TransactionTypeDTParameters parameters)
                {
                    return await transactionTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<TransactionType>> Search(string keyword)
                {
                    return await transactionTypeRepository.Search(keyword);
                }
        
                public async Task Update(TransactionType obj)
                {
                    await transactionTypeRepository.Update(obj);
                }
            }
        }
    
    