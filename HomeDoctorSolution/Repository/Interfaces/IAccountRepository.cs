
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Repository
{
    public interface IAccountRepository
    {
        Task<List<Account>> List();

        Task<List<Account>> Search(string keyword);

        Task<List<Account>> ListPaging(int pageIndex, int pageSize);

        Task<Account> Detail(int? postId);

        Task<Account> Add(Account Account);

        Task Update(Account Account);

        Task Delete(Account Account);

        Task<int> DeletePermanently(int? AccountId);

        int Count();

        Task<DTResult<AccountViewModel>> ListServerSide(AccountDTParameters parameters);
        Task<List<Account>> Login(Account obj);
        Task<Account?> GetByEmailAsync(string email);
        Task<Account> CheckCandidateByFacebookId(string facebookId);
        Task<Account> CheckCandidateByAppleId(string appleId);

        /// <summary>
        /// Author:TrungHieuTr
        /// Description: đăng ký tài khoản 
        /// </summary>
        /// <returns></returns>
        Task<Account> DetailByEmail(string? email);
        Task<bool> CheckUserNameExist(int id, string userName);
        Task<bool> EmailIsExisted(string email, int id);
        Task<bool> PhoneIsExisted(string phone, int id);
        Task<bool> CCCDIsExisted(string cccd, int id);
        //end Đăng ký 

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Reset mật khẩu
        /// </summary>
        /// <returns></returns>
        Task ResetNewPassword(Account obj);
        Task<bool> CheckEmailIsActive(string email);
        Task<Account> FindByEmail(string email);
        Task ChangePassword(Account obj);
        Task UpdateProfileAdmin(Account obj);
        Task<Account> GetAccountById(int id);
        //end reset mật khẩu

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<AccountAdminDTO>> DetailAccount(int? accountId);

        /// <summary>
        /// Author: HuyDQ
        /// Description: list danh sách theo roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<Account>> ListByRoleId(int roleId);
        Task UpdateAvatar(Account obj);

        Task<ReponseContactDTO> ListContact(int accountId, int pageIndex, int pageSize);
        Task<AccountProfileDTO> Profile(int? id);
        Task SetDevice(Account obj);
    }
}
