
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
            public class TransactionStatusService : ITransactionStatusService
            {
                ITransactionStatusRepository transactionStatusRepository;
                public TransactionStatusService(
                    ITransactionStatusRepository _transactionStatusRepository
                    )
                {
                    transactionStatusRepository = _transactionStatusRepository;
                }
                public async Task Add(TransactionStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await transactionStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = transactionStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(TransactionStatus obj)
                {
                    obj.Active = 0;
                    await transactionStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await transactionStatusRepository.DeletePermanently(id);
                }
        
                public async Task<TransactionStatus> Detail(int? id)
                {
                    return await transactionStatusRepository.Detail(id);
                }
        
                public async Task<List<TransactionStatus>> List()
                {
                    return await transactionStatusRepository.List();
                }
        
                public async Task<List<TransactionStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await transactionStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<TransactionStatus>> ListServerSide(TransactionStatusDTParameters parameters)
                {
                    return await transactionStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<TransactionStatus>> Search(string keyword)
                {
                    return await transactionStatusRepository.Search(keyword);
                }
        
                public async Task Update(TransactionStatus obj)
                {
                    await transactionStatusRepository.Update(obj);
                }
            }
        }
    
    