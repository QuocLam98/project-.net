
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IMedicalProfileService : IBaseService<MedicalProfile>
    {
        Task<DTResult<MedicalProfileViewModel>> ListServerSide(MedicalProfileDTParameters parameters);
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<HomeDoctorResponse> AddOrUpdateMedicalProfile(MedicalProfileViewModel model);
        Task<List<MedicalProfileViewModel>> MedicalProfile(int? accountId);
    }
}
