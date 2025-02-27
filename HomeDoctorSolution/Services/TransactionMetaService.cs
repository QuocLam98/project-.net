
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
            public class TransactionMetaService : ITransactionMetaService
            {
                ITransactionMetaRepository transactionMetaRepository;
                public TransactionMetaService(
                    ITransactionMetaRepository _transactionMetaRepository
                    )
                {
                    transactionMetaRepository = _transactionMetaRepository;
                }
                public async Task Add(TransactionMeta obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await transactionMetaRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = transactionMetaRepository.Count();
                    return result;
                }
        
                public async Task Delete(TransactionMeta obj)
                {
                    obj.Active = 0;
                    await transactionMetaRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await transactionMetaRepository.DeletePermanently(id);
                }
        
                public async Task<TransactionMeta> Detail(int? id)
                {
                    return await transactionMetaRepository.Detail(id);
                }
        
                public async Task<List<TransactionMeta>> List()
                {
                    return await transactionMetaRepository.List();
                }
        
                public async Task<List<TransactionMeta>> ListPaging(int pageIndex, int pageSize)
                {
                    return await transactionMetaRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<TransactionMeta>> ListServerSide(TransactionMetaDTParameters parameters)
                {
                    return await transactionMetaRepository.ListServerSide(parameters);
                }
        
                public async Task<List<TransactionMeta>> Search(string keyword)
                {
                    return await transactionMetaRepository.Search(keyword);
                }
        
                public async Task Update(TransactionMeta obj)
                {
                    await transactionMetaRepository.Update(obj);
                }
            }
        }
    
    