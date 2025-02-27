
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
    public interface IAccountMetaRepository
    {
        Task<List<AccountMeta>> List();

        Task<List<AccountMeta>> Search(string keyword);

        Task<List<AccountMeta>> ListPaging(int pageIndex, int pageSize);

        Task<AccountMeta> Detail(int? postId);

        Task<AccountMeta> Add(AccountMeta AccountMeta);

        Task Update(AccountMeta AccountMeta);

        Task Delete(AccountMeta AccountMeta);

        Task<int> DeletePermanently(int? AccountMetaId);

        int Count();

        Task<DTResult<AccountMetaViewModel>> ListServerSide(AccountMetaDTParameters parameters);


        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <returns></returns>
        Task<AccountMeta> GetAccountMeta(string key, string hash);
        Task<AccountMeta> GetAccountMetaByValue(int accountId, string value);
        Task DeActiveMeta(AccountMeta obj);
    }
}
