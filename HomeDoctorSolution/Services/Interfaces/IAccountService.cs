
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctorSolution.Models.ModelDTO;
namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IAccountService : IBaseService<Account>
    {
        Task<DTResult<AccountViewModel>> ListServerSide(AccountDTParameters parameters);
        Task<HomeDoctorResponse> Login(Account model);
        Task<HomeDoctorResponse> SignInWithApple(SignInWithSocialNetworkDTO obj);
        Task<HomeDoctorResponse> SignInWithFacebook(SignInWithSocialNetworkDTO obj);
        Task<HomeDoctorResponse> SignInWithGoole(SignInWithSocialNetworkDTO obj);

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: đăng ký tài khoản
        /// </summary>
        /// <returns></returns>
        Task<Account> DetailByEmail(string? email);
        Task<HomeDoctorResponse> Register(InsertAccountDTO obj);
        Task<bool> IsValidEmail(string email);
        Task<bool> CheckUserNameExist(int id, string userName);
        Task<List<string>> ValidAccount(string email, string phone, string cccd, int id);
        Task<List<string>> ValidAccount(string email, int id);
        Task UpdateAccount(Account obj);
        //end đăng ký

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Reset mật khẩu
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckKeyValid(string key, string hash);
        Task<bool> CheckKeyValidAndUpdateActiveAccount(string key, string hash);
        Task ResetNewPassword(string key, string hash, string password);
        Task<bool> CheckEmailIsActive(string email);
        Task<Account?> GetByEmailAsync(string email);
        Task<HomeDoctorResponse> ChangePasswordByForgot(ForgotPasswordDTO obj);

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Quên mật khẩu
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<HomeDoctorResponse> ForgotPassword(string value);
        Task<HomeDoctorResponse> ForgotPasswordEndUser(string value);
        Task<HomeDoctorResponse> ChangePassword(ChangePasswordDTO obj, int accountId);
        Task<HomeDoctorResponse> UpdateProfile(UpdateAdminAccountDTO obj, int accountId);
        Task<List<AccountAdminDTO>> DetailAccount(int? id);
        Task<AccountProfileDTO> GetProfile(int? id);

        /// <summary>
        /// Author: HuyDQ
        /// Description: list danh sách theo roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<Account>> ListByRoleId(int roleId);
        Task<HomeDoctorResponse> ChangeAvatar(Account model, IFormFile file);

        Task<ReponseContactDTO> ListContact(int accountId, int pageIndex, int pageSize);
    }
}

