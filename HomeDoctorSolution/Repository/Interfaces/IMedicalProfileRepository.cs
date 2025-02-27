
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
    public interface IMedicalProfileRepository
    {
        Task<List<MedicalProfile>> List();

        Task<List<MedicalProfile>> Search(string keyword);

        Task<List<MedicalProfile>> ListPaging(int pageIndex, int pageSize);

        Task<MedicalProfile> Detail(int? postId);

        Task<MedicalProfile> Add(MedicalProfile MedicalProfile);

        Task Update(MedicalProfile MedicalProfile);

        Task Delete(MedicalProfile MedicalProfile);

        Task<int> DeletePermanently(int? MedicalProfileId);

        int Count();

        Task<DTResult<MedicalProfileViewModel>> ListServerSide(MedicalProfileDTParameters parameters);
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<bool> CheckExistByAccountId(int? accountId);
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<MedicalProfile>> DetailByAccountId(int? accountId);
        Task<List<MedicalProfileViewModel>> MedicalProfile(int? accountId);
    }
}
