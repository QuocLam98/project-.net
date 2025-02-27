
using AutoMapper;
using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Email;
using HomeDoctorSolution.Util.Entities;
using HomeDoctorSolution.Util.Parameters;
using MathNet.Numerics.RootFinding;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace HomeDoctorSolution.Services
{
    public class AccountService : IAccountService
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        IAccountRepository accountRepository;
        ITokenService tokenService;
        IConfiguration config;
        IAccountMetaService accountMetaService;
        IAccountMetaRepository accountMetaRepository;
        IBookingRepository bookingRepository;
        private readonly IMapper _mapper;
        public AccountService(
            IAccountRepository _accountRepository,
            ITokenService _tokenService,
             IConfiguration _config,
            IAccountMetaService _accountMetaService,
            IAccountMetaRepository _accountMetaRepository,
            IMapper mapper,
            IBookingRepository _bookingRepository,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment env
            )
        {
            accountRepository = _accountRepository;
            tokenService = _tokenService;
            config = _config;
            accountMetaService = _accountMetaService;
            accountMetaRepository = _accountMetaRepository;
            _mapper = mapper;
            _env = env;
            bookingRepository = _bookingRepository;
        }
        public async Task Add(Account obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await accountRepository.Add(obj);
        }

        public int Count()
        {
            var result = accountRepository.Count();
            return result;
        }

        public async Task Delete(Account obj)
        {
            obj.Active = 0;
            await accountRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await accountRepository.DeletePermanently(id);
        }

        public async Task<Account> Detail(int? id)
        {
            return await accountRepository.Detail(id);
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<AccountAdminDTO>> DetailAccount(int? id)
        {
            return await accountRepository.DetailAccount(id);
        }

        public async Task<List<Account>> List()
        {
            return await accountRepository.List();
        }

        public async Task<List<Account>> ListPaging(int pageIndex, int pageSize)
        {
            return await accountRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<AccountViewModel>> ListServerSide(AccountDTParameters parameters)
        {
            return await accountRepository.ListServerSide(parameters);
        }

        public async Task<List<Account>> Search(string keyword)
        {
            return await accountRepository.Search(keyword);
        }

        public async Task Update(Account obj)
        {
            await accountRepository.Update(obj);
        }

        //created: JinDo
        //date: 15/12/2023
        //description: DangNhap
        public async Task<HomeDoctorResponse> Login(Account model)
        {
            //trim
            model.Username = model.Username.Trim();
            model.Password = model.Password.Trim();
            if (model.Username.Length == 0 || model.Password.Length == 0)
            {
                return HomeDoctorResponse.BAD_REQUEST();

            }

            var dataList = await accountRepository.Login(model);
            if (dataList[0].IsActivated == 0)
            {
               return  HomeDoctorResponse.BAD_REQUEST("Tài khoản của bạn chưa được kích hoạt, vui lòng kiểm tra email để kích hoạt tài khoản.");
            }
            if (dataList[0].Password == SecurityUtil.ComputeSha256Hash(model.Password))
            {
                string accessToken = tokenService.GenerateToken(new AccountToken()//get access token
                {
                    Email = dataList[0].Email,
                    Id = dataList[0].Id,
                    Username = dataList[0].Username,
                    RoleId = dataList[0].RoleId,
                }, DateTime.Now.AddMinutes(int.Parse(config["Jwt:AdminExpireMinutes"])));
                var mapToAccount = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Account, AccountProfileResponseDTO>();
                });
                var account = mapToAccount.CreateMapper().Map<AccountProfileResponseDTO>(dataList[0]);
                //account.Name = dataList[0].FirstName + " " + dataList[0].LastName;
                //Lấy thông tin booking của tài khoản
                var listbooking = await bookingRepository.ListBookingByAccountId(dataList[0].Id);
                var countBookingWait = 0;
                if (listbooking != null)
                {
                    for (int i = 0; i < listbooking.Count; i++)
                    {
                        if (listbooking[i].BookingStatusId == BookingStatusId.WAIT || listbooking[i].BookingStatusId == BookingStatusId.ACCEPT)
                        {
                            countBookingWait += 1;
                        }
                    }
                }
                return HomeDoctorResponse.Success(new SignInResponse()//Change resources
                {
                    AccessToken = accessToken,
                    Profile = account,
                    CountBookingWait = countBookingWait

                });
            }
            else
            {
                return HomeDoctorResponse.BAD_REQUEST();
            }
        }
        //Chức năng : Đăng nhập bằng tài khoản Google
        public async Task<HomeDoctorResponse> SignInWithGoole(SignInWithSocialNetworkDTO obj)
        {
            if (!EmailValidate(obj.Email))
            {
                return HomeDoctorResponse.BAD_REQUEST("Email không hợp lệ.");
            }
            var account = await accountRepository.GetByEmailAsync(obj.Email);
            if (account == null)
            {
                var newAccount = new Account()
                {
                    Active = 1,
                    Email = obj.Email,
                    CreatedTime = DateTime.Now,
                    Password = "",
                    GoogleId = obj.Id,
                    Photo = obj.Photo != null ? obj.Photo : AccountConstant.PHOTO_DEFAULT,
                    Phone = "",
                    Id = 0,
                    RoleId = RoleId.USER,
                    AccountStatusId = AccountConstant.ACCOUNT_STATUS_DEFAULT,
                    AccountTypeId = AccountConstant.ACCOUNT_TYPE_DEFAULT,
                    IsActivated = 1,
                    Username = obj.FullName,
                    Name = obj.FullName,
                    FirstName = "",
                    LastName = "",
                };

                await accountRepository.Add(newAccount);

                AccountToken accountToken = new()
                {
                    Id = newAccount.Id,
                    Email = newAccount.Email,
                    Phone = newAccount.Phone,
                    Username = newAccount.Name,
                    RoleId = newAccount.RoleId,
                };
                string token = tokenService.GenerateToken(accountToken);
                LoginAccountModel loginCandidate = new()
                {
                    Name = newAccount.Name,
                    Photo = newAccount.Photo,
                    Phone = newAccount.Phone,
                    Token = token,
                };

                return HomeDoctorResponse.Success(loginCandidate);
            }
            else
            {
                if (account.Active == 1)
                {
                    account.GoogleId = obj.Id;
                    await accountRepository.Update(account);

                    AccountToken accountToken = new()
                    {
                        Id = account.Id,
                        Email = account.Email,
                        Phone = account.Phone,
                        Username = account.Name
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    LoginAccountModel loginCandidate = new()
                    {
                        Name = account.Name,
                        Photo = account.Photo,
                        Phone = account.Phone,
                        Token = token,
                    };

                    return HomeDoctorResponse.SUCCESS(loginCandidate);
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST("Tài khoản của bạn chưa được kích hoạt, vui lòng kiểm tra email để kích hoạt tài khoản.");
                }
            }
        }
        //Chức năng : Đăng nhập bằng tài khoản Facebook
        public async Task<HomeDoctorResponse> SignInWithFacebook(SignInWithSocialNetworkDTO obj)
        {
            var account = new Account();
            if (obj.Email.Contains("@"))
            {
                if (!EmailValidate(obj.Email))
                {
                    return HomeDoctorResponse.BAD_REQUEST("Email không hợp lệ.");

                }

                account = await accountRepository.GetByEmailAsync(obj.Email);
            }
            else
            {
                account = await accountRepository.CheckCandidateByFacebookId(obj.Id);
            }

            if (account == null)
            {
                var newAccount = new Account()
                {
                    Active = 1,
                    Email = "",
                    CreatedTime = DateTime.Now,
                    Password = "",
                    FacebookId = obj.Id,
                    Photo = obj.Photo != null ? obj.Photo : AccountConstant.PHOTO_DEFAULT,
                    Phone = "",
                    Id = 0,
                    RoleId = RoleId.USER,
                    AccountStatusId = AccountConstant.ACCOUNT_STATUS_DEFAULT,
                    AccountTypeId = AccountConstant.ACCOUNT_TYPE_DEFAULT,
                    IsActivated = 1,
                    Username = obj.FullName,
                    Name = obj.FullName,
                    ClassId = AccountConstant.CLASS_DEFAULT_ID,
                    SchoolId = AccountConstant.SCHOOL_DEFAULT_ID,
                    FirstName = "",
                    LastName = "",
                };

                await accountRepository.Add(newAccount);

                if (newAccount.Id > 0)
                {
                    AccountToken accountToken = new()
                    {
                        Id = newAccount.Id,
                        Email = newAccount.Email,
                        Phone = newAccount.Phone,
                        Username = newAccount.Name
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    LoginAccountModel loginCandidate = new()
                    {
                        Name = newAccount.Name,
                        Photo = newAccount.Photo,
                        Phone = newAccount.Phone,
                        Token = token,
                    };

                    return HomeDoctorResponse.SUCCESS(loginCandidate);
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST("Có lỗi khi đăng nhập tài khoản, vui lòng thử lại.");
                }
            }
            else
            {
                if (account.Active == 1)
                {
                    account.FacebookId = obj.Id;
                    await accountRepository.Update(account);

                    AccountToken accountToken = new()
                    {
                        Id = account.Id,
                        Email = account.Email,
                        Phone = account.Phone,
                        Username = account.Name
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    LoginAccountModel loginCandidate = new()
                    {
                        Name = account.Name,
                        Photo = account.Photo,
                        Phone = account.Phone,
                        Token = token,
                    };

                    return HomeDoctorResponse.SUCCESS(loginCandidate);
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST("Tài khoản của bạn chưa được kích hoạt, vui lòng kiểm tra email để kích hoạt tài khoản.");
                }
            }
        }
        //Chức năng : Đăng nhập bằng tài khoản Apple
        public async Task<HomeDoctorResponse> SignInWithApple(SignInWithSocialNetworkDTO obj)
        {
            var account = new Account();
            if (obj.Email.Contains("@"))
            {
                if (!EmailValidate(obj.Email))
                {
                    return HomeDoctorResponse.BAD_REQUEST("Email không hợp lệ.");

                }

                account = await accountRepository.GetByEmailAsync(obj.Email);
            }
            else
            {
                account = await accountRepository.CheckCandidateByAppleId(obj.Id);
            }

            if (account == null)
            {
                var newAccount = new Account()
                {
                    Active = 1,
                    Email = obj.Email,
                    CreatedTime = DateTime.Now,
                    Password = "",
                    AppleId = obj.Id,
                    Photo = AccountConstant.PHOTO_DEFAULT,
                    Phone = "",
                    Id = 0,
                    RoleId = RoleId.USER,
                    AccountStatusId = AccountConstant.ACCOUNT_STATUS_DEFAULT,
                    AccountTypeId = AccountConstant.ACCOUNT_TYPE_DEFAULT,
                    IsActivated = 1,
                    Username = obj.FullName,
                    Name = obj.FullName,
                    ClassId = AccountConstant.CLASS_DEFAULT_ID,
                    SchoolId = AccountConstant.SCHOOL_DEFAULT_ID,
                    FirstName = "",
                    LastName = "",
                };

                await accountRepository.Add(newAccount);

                if (newAccount.Id > 0)
                {
                    AccountToken accountToken = new()
                    {
                        Id = newAccount.Id,
                        Email = newAccount.Email,
                        Phone = newAccount.Phone,
                        Username = newAccount.Name
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    LoginAccountModel loginCandidate = new()
                    {
                        Fullname = newAccount.Name,
                        Photo = newAccount.Photo,
                        Token = token,
                    };

                    return HomeDoctorResponse.SUCCESS(loginCandidate);
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST("Có lỗi khi đăng nhập tài khoản, vui lòng thử lại.");
                }
            }
            else
            {
                if (account.Active == 1)
                {
                    account.AppleId = obj.Id;
                    await accountRepository.Update(account);

                    AccountToken accountToken = new()
                    {
                        Id = account.Id,
                        Email = account.Email,
                        Phone = account.Phone,
                        Username = account.Name
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    LoginAccountModel loginCandidate = new()
                    {
                        Fullname = account.Name,
                        Photo = account.Photo,
                        Token = token,
                    };

                    return HomeDoctorResponse.SUCCESS(loginCandidate);
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST("Tài khoản của bạn chưa được kích hoạt, vui lòng kiểm tra email để kích hoạt tài khoản.");
                }
            }
        }
        public bool EmailValidate(string email)
        {
            bool isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        //end DangNhap

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: đăng ký tài khoản
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public async Task<Account> DetailByEmail(string? email)
        {
            return await accountRepository.DetailByEmail(email);
        }

        public async Task<HomeDoctorResponse> Register(InsertAccountDTO obj)
        {
            var homedoctorsolutionResponse = HomeDoctorResponse.BAD_REQUEST();
            var code = HappySUtil.RandomSecurityNumber(999999);
            var hash = HappySUtil.RandomSecurityString(32);
            //1. business logic
            var checkUserName = await CheckUserNameExist(0, obj.Username);
            if (checkUserName)
            {
                var existUserNameObj = HomeDoctorResponse.UsernameExist(checkUserName);
                return existUserNameObj;
            }
            var checkEmail = await IsValidEmail(obj.Email);
            if (!checkEmail)
            {
                var existEmailValid = HomeDoctorResponse.EmailNotValid(checkEmail);
                return existEmailValid;
            }
            var erors = await ValidAccount(obj.Email, 0);
            if (erors.Count > 0)
            {
                var exist = HomeDoctorResponse.EmailExist(erors);
                return exist;
            }
            try
            {
                string password = SecurityUtil.ComputeSha256Hash(obj.Password);
                obj.Password = password;
                var mapToAccount = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InsertAccountDTO, Account>();
                });
                var account = mapToAccount.CreateMapper().Map<Account>(obj);
                account.Id = 0;
                account.Name = account.FirstName + " " + account.MiddleName + " " + account.LastName;
                account.AccountStatusId = SystemConstant.ACCOUNT_STATUS_ACTIVE;
                account.AccountTypeId = SystemConstant.ACCOUNT_TYPE_DEFAULT;
                account.RoleId = SystemConstant.ROLE_USER;
                account.Active = 1;
                account.IsActivated = 0;
                account.Dob = DateTime.Now;
                account.CreatedTime = DateTime.Now;
                await accountRepository.Add(account);
                Account AccountDataList = await DetailByEmail(account.Email);
                if (AccountDataList != null)
                {
                    //Thêm bản ghi vào accountmeta
                    var accountMeta = new AccountMeta();
                    accountMeta.Id = 0;
                    accountMeta.Name = account.Email;
                    accountMeta.AccountId = AccountDataList.Id;
                    accountMeta.Value = code;
                    accountMeta.Key = SystemConstant.REGISTER_ACCOUNT;
                    accountMeta.Description = hash;
                    await accountMetaService.Add(accountMeta);
                    //Cấu hình gửi mail
                    string url = BaseController.SystemURL;
                    string RandomSecurityString = url + "xac-thuc-tai-khoan/" + code + "/" + hash;
                    string body = EmailUtil.EmailRegister(account.Name, RandomSecurityString);
                    EmailUtil.SendEmail(account.Email, "Yêu cầu xác thực tài khoản", body);
                    var homeDoctorResponse = HomeDoctorResponse.SUCCESS();
                    return homeDoctorResponse;
                }

                homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(obj);
                return homedoctorsolutionResponse;
            }
            catch (Exception e)
            {
                homedoctorsolutionResponse = HomeDoctorResponse.BAD_REQUEST();
                return homedoctorsolutionResponse;
            }
        }
        #region Check valid
        public async Task<bool> CheckUserNameExist(int id, string userName)
        {
            return await accountRepository.CheckUserNameExist(id, userName);
        }

        public async Task<List<string>> ValidAccount(string email, string phone, string cccd, int id)
        {
            var erors = new List<string>();
            if (!String.IsNullOrEmpty(email))
            {
                if (await accountRepository.EmailIsExisted(email, id))
                {
                    erors.Add("Email đã tồn tại");
                }
            }
            if (!String.IsNullOrEmpty(phone))
            {
                if (await accountRepository.PhoneIsExisted(phone, id))
                {
                    erors.Add("Số điện thoại đã tồn tại");
                }
            }
            if (!String.IsNullOrEmpty(cccd))
            {
                if (await accountRepository.CCCDIsExisted(cccd, id))
                {
                    erors.Add("CCCD đã tồn tại");
                }
            }
            return erors;
        }
        public async Task<List<string>> ValidAccount(string email, int id)
        {
            var erors = new List<string>();
            if (await accountRepository.EmailIsExisted(email, id))
            {
                erors.Add("Email đã tồn tại");
            }
            return erors;
        }

        public async Task UpdateAccount(Account obj)
        {
            var detailAcc = await accountRepository.Detail(obj.Id);
            if (detailAcc != null)
            {
                if (!(detailAcc.Password == obj.Password))
                {
                    obj.Password = SecurityUtil.ComputeSha256Hash(obj.Password);
                }
            }
            await accountRepository.Update(obj);
        }
        #endregion
        // end Đăng ký

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Reset mật khẩu
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckKeyValid(string key, string hash)
        {
            var result = false;
            var accmetum = await accountMetaRepository.GetAccountMeta(key, hash);
            if (accmetum != null)
            {
                var time = DateTime.Now - accmetum.CreatedTime;
                if (time < TimeSpan.FromHours(SystemConstant.FORGOT_PASSSWORD_TOKEN_EXPRIED))
                {
                    result = true;
                }
            }
            return result;
        }
        public async Task<bool> CheckKeyValidAndUpdateActiveAccount(string key, string hash)
        {
            var result = false;
            var accmetum = await accountMetaRepository.GetAccountMeta(key, hash);
            if (accmetum != null)
            {
                var accObj = await Detail(accmetum.AccountId);
                if (accObj != null && accObj.IsActivated == 0)
                {
                    accObj.IsActivated = 1;
                    await Update(accObj);
                    var time = DateTime.Now - accmetum.CreatedTime;
                    if (time < TimeSpan.FromHours(SystemConstant.FORGOT_PASSSWORD_TOKEN_EXPRIED))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public async Task ResetNewPassword(string key, string hash, string password)
        {
            var accMeta = await accountMetaRepository.GetAccountMeta(key, hash);
            var acc = await accountRepository.GetAccountById(accMeta.AccountId);
            acc.Password = password;
            await accountRepository.ResetNewPassword(acc);
            await accountMetaRepository.DeActiveMeta(accMeta);

        }
        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await accountRepository.CheckEmailIsActive(email);
        }
        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await accountRepository.FindByEmail(email);
        }
        public async Task<HomeDoctorResponse> ChangePasswordByForgot(ForgotPasswordDTO obj)
        {
            //trim
            obj.NewPassword = obj.NewPassword.Trim();
            if (obj.NewPassword.Length == 0)
            {
                return HomeDoctorResponse.BAD_REQUEST();
            }
            var dataList = await accountRepository.DetailByEmail(obj.Email);
            if (dataList != null)
            {
                var objMeta = await accountMetaRepository.GetAccountMetaByValue(dataList.Id, obj.Hash);
                if (objMeta != null)
                {
                    var mapToAccount = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<ForgotPasswordDTO, Account>();
                    });
                    var account = mapToAccount.CreateMapper().Map<Account>(obj);
                    account.Id = dataList.Id;
                    account.Password = SecurityUtil.ComputeSha256Hash(obj.NewPassword);
                    await accountRepository.ChangePassword(account);
                    objMeta.Active = 0;
                    await accountMetaRepository.DeActiveMeta(objMeta);
                    obj = new ForgotPasswordDTO();
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(obj);
                    return homedoctorsolutionResponse;
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST();
                }
            }
            else
            {
                return HomeDoctorResponse.BAD_REQUEST();
            }
        }


        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Quên mật khẩu
        /// </summary>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> ForgotPassword(string value)
        {
            var code = HappySUtil.RandomSecurityNumber(999999);
            var hash = HappySUtil.RandomSecurityString(32);
            Account AccountDataList = await DetailByEmail(value);
            if (AccountDataList != null)
            {
                //Thêm bản ghi vào accountmeta
                var accountMeta = new AccountMeta();
                accountMeta.Id = 0;
                accountMeta.Name = value;
                accountMeta.AccountId = AccountDataList.Id;
                accountMeta.Value = code;
                accountMeta.Key = SystemConstant.FORGOT_PASSWORD_EMAIL;
                accountMeta.Description = hash;
                await accountMetaService.Add(accountMeta);
                //Cấu hình gửi mail
                string url = BaseController.SystemURL;
                string RandomSecurityString = url + "admin/action/doi-mat-khau/" + code + "/" + hash;
                string body = EmailUtil.EmailForgotPassword(AccountDataList.Name, RandomSecurityString);
                EmailUtil.SendEmail(value, "Yêu cầu đổi mật khẩu", body);
                var homeDoctorResponse = HomeDoctorResponse.SUCCESS();
                return homeDoctorResponse;
            }
            else
            {
                var homeDoctorResponse = HomeDoctorResponse.BAD_REQUEST();
                return homeDoctorResponse;
            }
        }
        public async Task<HomeDoctorResponse> ForgotPasswordEndUser(string value)
        {
            var code = HappySUtil.RandomSecurityNumber(999999);
            var hash = HappySUtil.RandomSecurityString(32);
            Account AccountDataList = await DetailByEmail(value);
            if (AccountDataList != null)
            {
                //Thêm bản ghi vào accountmeta
                var accountMeta = new AccountMeta();
                accountMeta.Id = 0;
                accountMeta.Name = value;
                accountMeta.AccountId = AccountDataList.Id;
                accountMeta.Value = code;
                accountMeta.Key = SystemConstant.FORGOT_PASSWORD_EMAIL;
                accountMeta.Description = hash;
                await accountMetaService.Add(accountMeta);
                //Cấu hình gửi mail
                string url = BaseController.SystemURL;
                string RandomSecurityString = url + "admin/action/doi-mat-khau/" + code + "/" + hash;
                string body = EmailUtil.EmailForgotPassword(AccountDataList.Name, RandomSecurityString, code);
                EmailUtil.SendEmail(value, "Yêu cầu đổi mật khẩu", body);
                var homeDoctorResponse = HomeDoctorResponse.SUCCESS();
                return homeDoctorResponse;
            }
            else
            {
                var homeDoctorResponse = HomeDoctorResponse.BAD_REQUEST();
                return homeDoctorResponse;
            }
        }
        public async Task<HomeDoctorResponse> ChangePassword(ChangePasswordDTO obj, int accountId)
        {
            //trim
            obj.Password = obj.Password.Trim();
            if (obj.Password.Length == 0 || obj.NewPassword.Length == 0)
            {
                return HomeDoctorResponse.BAD_REQUEST();
            }

            var dataList = await accountRepository.Detail(accountId);
            if (dataList.Password == SecurityUtil.ComputeSha256Hash(obj.Password))
            {
                if(dataList.Password != SecurityUtil.ComputeSha256Hash(obj.NewPassword))
                {
                    var accountChange = _mapper.Map<Account>(obj);
                    accountChange.Id = dataList.Id;
                    accountChange.Password = SecurityUtil.ComputeSha256Hash(obj.NewPassword);
                    await accountRepository.ChangePassword(accountChange);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(obj);
                    return homedoctorsolutionResponse; ;
                }
                else
                {
                    return HomeDoctorResponse.BAD_REQUEST("Mật khẩu mới trùng với mật khẩu cũ");
                }
            }
            else
            {
                return HomeDoctorResponse.BAD_REQUEST("Mật khẩu cũ chưa trùng khớp");
            }
        }
        public async Task<HomeDoctorResponse> UpdateProfile(UpdateAdminAccountDTO obj, int accountId)
        {
            //trim
            obj.Name = obj.Name.Trim();
            if (obj.Name.Length == 0 || obj.Name.Length == 0)
            {
                return HomeDoctorResponse.BAD_REQUEST();
            }

            try
            {
                var dataList = await accountRepository.Detail(accountId);
                var accountChange = _mapper.Map<Account>(obj);
                accountChange.Id = dataList.Id;
                await accountRepository.UpdateProfileAdmin(accountChange);
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(obj);
                return homedoctorsolutionResponse;
            }
            catch(Exception ex)
            {
                return HomeDoctorResponse.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Author: HuyDQ
        /// Description: list danh sách theo roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<Account>> ListByRoleId(int roleId)
        {
            return await accountRepository.ListByRoleId(roleId);
        }
        public async Task<HomeDoctorResponse> ChangeAvatar(Account model, IFormFile file)
        {
            Account objAc = await accountRepository.Detail(model.Id);
            if (objAc != null)
            {
                string[] arrExtension = { ".jpg", ".jpeg", ".bmp", ".gif", ".png",
                ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".pps", ".ppt", ".pptx", ".heif" };
                string fileName = file.FileName;
                string name = file.FileName.Substring(0, file.FileName.LastIndexOf("."));
                string extension = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                string _dir = _env.ContentRootPath + "\\wwwroot\\files\\upload\\account\\";
                fileName = name + "-" + extension;
                if (Array.IndexOf(arrExtension, extension) > -1)
                {
                    //Chỗ này kiểm tra xem có folder chưa nếu chưa có thì tạo
                    if (!Directory.Exists(_dir))
                    {
                        Directory.CreateDirectory(_dir);
                    }
                    using (var fileStream = new FileStream(Path.Combine(_dir, fileName), FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                objAc.Photo = "\\files\\upload\\account\\" + fileName;
                await accountRepository.UpdateAvatar(objAc);
                var happySmileResponse = HomeDoctorResponse.SUCCESS(objAc.Photo);
                return happySmileResponse;
            }
            else
            {
                var happySmileResponse = HomeDoctorResponse.BAD_REQUEST();
                return happySmileResponse;
            }
        }

        public async Task<ReponseContactDTO> ListContact(int accountId, int pageIndex, int pageSize)
        {
            return await accountRepository.ListContact(accountId, pageIndex, pageSize);
        }
        public async Task<AccountProfileDTO> GetProfile(int? id)
        {
            var dataList = await accountRepository.Profile(id);
            return dataList;
        }
    }
}

