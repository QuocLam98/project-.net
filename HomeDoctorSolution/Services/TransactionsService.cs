
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
            public class TransactionsService : ITransactionsService
            {
                ITransactionsRepository transactionsRepository;
                public TransactionsService(
                    ITransactionsRepository _transactionsRepository
                    )
                {
                    transactionsRepository = _transactionsRepository;
                }
                public async Task Add(Transaction obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await transactionsRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = transactionsRepository.Count();
                    return result;
                }
        
                public async Task Delete(Transaction obj)
                {
                    obj.Active = 0;
                    await transactionsRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await transactionsRepository.DeletePermanently(id);
                }
        
                public async Task<Transaction> Detail(int? id)
                {
                    return await transactionsRepository.Detail(id);
                }
        
                public async Task<List<Transaction>> List()
                {
                    return await transactionsRepository.List();
                }
        
                public async Task<List<Transaction>> ListPaging(int pageIndex, int pageSize)
                {
                    return await transactionsRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<TransactionsViewModel>> ListServerSide(TransactionsDTParameters parameters)
                {
                    return await transactionsRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Transaction>> Search(string keyword)
                {
                    return await transactionsRepository.Search(keyword);
                }
        
                public async Task Update(Transaction obj)
                {
                    await transactionsRepository.Update(obj);
                }
            }
        }
    
    