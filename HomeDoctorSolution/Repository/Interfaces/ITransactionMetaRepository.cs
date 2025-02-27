
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;


namespace HomeDoctorSolution.Repository
{
    public interface ITransactionMetaRepository
    {
        Task<List<TransactionMeta>> List();

        Task<List<TransactionMeta>> Search(string keyword);

        Task<List<TransactionMeta>> ListPaging(int pageIndex, int pageSize);

        Task<TransactionMeta> Detail(int? postId);

        Task<TransactionMeta> Add(TransactionMeta TransactionMeta);

        Task Update(TransactionMeta TransactionMeta);

        Task Delete(TransactionMeta TransactionMeta);

        Task<int> DeletePermanently(int? TransactionMetaId);

        int Count();

        Task<DTResult<TransactionMeta>> ListServerSide(TransactionMetaDTParameters parameters);
    }
}
