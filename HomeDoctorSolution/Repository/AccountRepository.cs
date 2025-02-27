using HomeDoctorSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Globalization;
using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Models.ModelDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NPOI.HSSF.Record.Chart;

namespace HomeDoctorSolution.Repository
{
    public class AccountRepository : IAccountRepository
    {
        HomeDoctorContext db;

        public AccountRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Account>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Accounts
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Account>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Accounts
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Account>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Accounts
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }


        public async Task<Account> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Accounts.AsNoTracking().FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }

            return null;
        }


        public async Task<Account> Add(Account obj)
        {
            if (db != null)
            {
                await db.Accounts.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }

            return null;
        }


        public async Task Update(Account obj)
        {
            if (db != null)
            {
                //Update that object
                db.Accounts.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = false;
                db.Entry(obj).Property(x => x.IsActivated).IsModified = true;
                db.Entry(obj).Property(x => x.GuId).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.FirstName).IsModified = true;
                db.Entry(obj).Property(x => x.LastName).IsModified = true;
                db.Entry(obj).Property(x => x.MiddleName).IsModified = true;
                db.Entry(obj).Property(x => x.Email).IsModified = true;
                db.Entry(obj).Property(x => x.Username).IsModified = true;
                db.Entry(obj).Property(x => x.Password).IsModified = true;
                db.Entry(obj).Property(x => x.Phone).IsModified = true;
                db.Entry(obj).Property(x => x.Address).IsModified = true;
                db.Entry(obj).Property(x => x.GoogleId).IsModified = true;
                db.Entry(obj).Property(x => x.FacebookId).IsModified = true;
                db.Entry(obj).Property(x => x.Dob).IsModified = true;
                db.Entry(obj).Property(x => x.Gender).IsModified = true;
                db.Entry(obj).Property(x => x.IdCardNumber).IsModified = true;
                db.Entry(obj).Property(x => x.IdCardPhoto1).IsModified = true;
                db.Entry(obj).Property(x => x.IdCardPhoto2).IsModified = true;
                db.Entry(obj).Property(x => x.IdCardGrantedDate).IsModified = true;
                db.Entry(obj).Property(x => x.IdCardGrantedPlace).IsModified = true;
                db.Entry(obj).Property(x => x.AddressCity).IsModified = true;
                db.Entry(obj).Property(x => x.AddressDistrict).IsModified = true;
                db.Entry(obj).Property(x => x.AddressWard).IsModified = true;
                db.Entry(obj).Property(x => x.AddressDetail).IsModified = true;
                db.Entry(obj).Property(x => x.LinkedAccount).IsModified = true;
                db.Entry(obj).Property(x => x.LinkedPassword).IsModified = true;
                db.Entry(obj).Property(x => x.BankName).IsModified = true;
                db.Entry(obj).Property(x => x.BankNumber).IsModified = true;
                db.Entry(obj).Property(x => x.BankBranch).IsModified = true;
                db.Entry(obj).Property(x => x.BankNote).IsModified = true;
                db.Entry(obj).Property(x => x.RoleId).IsModified = true;
                db.Entry(obj).Property(x => x.AccountTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.AccountStatusId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Account obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Accounts.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePermanently(int? objId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the obj for specific obj id
                var obj = await db.Accounts.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Accounts.Remove(obj);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }

                return result;
            }

            return result;
        }


        public int Count()
        {
            int result = 0;

            if (db != null)
            {
                //Find the obj for specific obj id
                result = (
                    from row in db.Accounts
                    where row.Active == 1
                    select row
                ).Count();
            }

            return result;
        }

        public async Task<DTResult<AccountViewModel>> ListServerSide(AccountDTParameters parameters)
        {
            //0. Options
            string searchAll = parameters.SearchAll.Trim(); //Trim text
            string orderCritirea = "Id"; //Set default critirea
            int recordTotal, recordFiltered;
            bool orderDirectionASC = true; //Set default ascending
            if (parameters.Order != null)
            {
                orderCritirea = parameters.Columns[parameters.Order[0].Column].Data;
                orderDirectionASC = parameters.Order[0].Dir == DTOrderDir.ASC;
            }

            //1. Join
            var query = from row in db.Accounts
                join r in db.Roles on row.RoleId equals r.Id
                join at in db.AccountTypes on row.AccountTypeId equals at.Id
                join ast in db.AccountStatuses on row.AccountStatusId equals ast.Id
                where row.Active == 1
                      && r.Active == 1
                      && at.Active == 1
                      && ast.Active == 1
                select new
                {
                    row,
                    r,
                    at,
                    ast
                };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    c.row.Id.ToString().Contains(searchAll) ||
                    EF.Functions.Collate(c.row.Name, SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    c.row.Email.ToLower().Contains(searchAll) ||
                    c.row.Username.ToLower().Contains(searchAll) ||
                    c.row.Phone.Contains(searchAll) ||
                    c.row.CreatedTime.ToCustomString().Contains(searchAll)
                );
            }

            foreach (var item in parameters.Columns)
            {
                var fillter = item.Search.Value.Trim();
                if (fillter.Length > 0)
                {
                    switch (item.Data)
                    {
                        case "id":
                            query = query.Where(c => c.row.Id.ToString().Trim().Contains(fillter));
                            break;
                        case "active":
                            query = query.Where(c => c.row.Active.ToString().Trim().Contains(fillter));
                            break;
                        case "isActivated":
                            query = query.Where(c => c.row.IsActivated.ToString().Trim().Contains(fillter));
                            break;
                        case "guId":
                            query = query.Where(c => c.row.GuId.ToString().Trim().Contains(fillter));
                            break;
                        case "name":
                            query = query.Where(c => (c.row.Name ?? "").Contains(fillter));
                            break;
                        case "description":
                            query = query.Where(c => (c.row.Description ?? "").Contains(fillter));
                            break;
                        case "info":
                            query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
                            break;
                        case "photo":
                            query = query.Where(c => (c.row.Photo ?? "").Contains(fillter));
                            break;
                        case "firstName":
                            query = query.Where(c => (c.row.FirstName ?? "").Contains(fillter));
                            break;
                        case "lastName":
                            query = query.Where(c => (c.row.LastName ?? "").Contains(fillter));
                            break;
                        case "middleName":
                            query = query.Where(c => (c.row.MiddleName ?? "").Contains(fillter));
                            break;
                        case "email":
                            query = query.Where(c => (c.row.Email ?? "").Contains(fillter));
                            break;
                        case "username":
                            query = query.Where(c => (c.row.Username ?? "").Contains(fillter));
                            break;
                        case "password":
                            query = query.Where(c => (c.row.Password ?? "").Contains(fillter));
                            break;
                        case "phone":
                            query = query.Where(c => (c.row.Phone ?? "").Contains(fillter));
                            break;
                        case "address":
                            query = query.Where(c => (c.row.Address ?? "").Contains(fillter));
                            break;
                        case "googleId":
                            query = query.Where(c => (c.row.GoogleId ?? "").Contains(fillter));
                            break;
                        case "facebookId":
                            query = query.Where(c => (c.row.FacebookId ?? "").Contains(fillter));
                            break;
                        case "dob":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                    .AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.Dob >= startDate && c.row.Dob <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.Dob.Value.Date == date.Date);
                            }

                            break;
                        case "gender":
                            query = query.Where(c => (c.row.Gender ?? "").Contains(fillter));
                            break;
                        case "idCardNumber":
                            query = query.Where(c => (c.row.IdCardNumber ?? "").Contains(fillter));
                            break;
                        case "idCardPhoto1":
                            query = query.Where(c => (c.row.IdCardPhoto1 ?? "").Contains(fillter));
                            break;
                        case "idCardPhoto2":
                            query = query.Where(c => (c.row.IdCardPhoto2 ?? "").Contains(fillter));
                            break;
                        case "idCardGrantedDate":
                            query = query.Where(c => (c.row.IdCardGrantedDate ?? "").Contains(fillter));
                            break;
                        case "idCardGrantedPlace":
                            query = query.Where(c => (c.row.IdCardGrantedPlace ?? "").Contains(fillter));
                            break;
                        case "addressCity":
                            query = query.Where(c => (c.row.AddressCity ?? "").Contains(fillter));
                            break;
                        case "addressDistrict":
                            query = query.Where(c => (c.row.AddressDistrict ?? "").Contains(fillter));
                            break;
                        case "addressWard":
                            query = query.Where(c => (c.row.AddressWard ?? "").Contains(fillter));
                            break;
                        case "addressDetail":
                            query = query.Where(c => (c.row.AddressDetail ?? "").Contains(fillter));
                            break;
                        case "linkedAccount":
                            query = query.Where(c => (c.row.LinkedAccount ?? "").Contains(fillter));
                            break;
                        case "linkedPassword":
                            query = query.Where(c => (c.row.LinkedPassword ?? "").Contains(fillter));
                            break;
                        case "bankName":
                            query = query.Where(c => (c.row.BankName ?? "").Contains(fillter));
                            break;
                        case "bankNumber":
                            query = query.Where(c => (c.row.BankNumber ?? "").Contains(fillter));
                            break;
                        case "bankBranch":
                            query = query.Where(c => (c.row.BankBranch ?? "").Contains(fillter));
                            break;
                        case "bankNote":
                            query = query.Where(c => (c.row.BankNote ?? "").Contains(fillter));
                            break;
                        case "createdTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                    .AddDays(1).AddSeconds(-1);
                                query = query.Where(c =>
                                    c.row.CreatedTime >= startDate && c.row.CreatedTime <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.CreatedTime.Date == date.Date);
                            }

                            break;
                        case "roleId":
                            if (fillter != "null")
                            {
                                query = query.Where(x => x.row.RoleId == int.Parse(fillter));
                            }

                            break;
                    }
                }
            }

            if (parameters.RoleIds.Count > 0)
            {
                query = query.Where(c => parameters.RoleIds.Contains(c.row.Role.Id));
            }


            if (parameters.AccountTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountTypeIds.Contains(c.row.AccountType.Id));
            }


            if (parameters.AccountStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountStatusIds.Contains(c.row.AccountStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new AccountViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                IsActivated = c.row.IsActivated,
                GuId = c.row.GuId,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                Photo = c.row.Photo,
                FirstName = c.row.FirstName,
                LastName = c.row.LastName,
                MiddleName = c.row.MiddleName,
                Email = c.row.Email,
                Username = c.row.Username,
                Password = c.row.Password,
                Phone = c.row.Phone,
                Address = c.row.Address,
                GoogleId = c.row.GoogleId,
                FacebookId = c.row.FacebookId,
                Dob = c.row.Dob,
                Gender = c.row.Gender,
                IdCardNumber = c.row.IdCardNumber,
                IdCardPhoto1 = c.row.IdCardPhoto1,
                IdCardPhoto2 = c.row.IdCardPhoto2,
                IdCardGrantedDate = c.row.IdCardGrantedDate,
                IdCardGrantedPlace = c.row.IdCardGrantedPlace,
                AddressCity = c.row.AddressCity,
                AddressDistrict = c.row.AddressDistrict,
                AddressWard = c.row.AddressWard,
                AddressDetail = c.row.AddressDetail,
                LinkedAccount = c.row.LinkedAccount,
                LinkedPassword = c.row.LinkedPassword,
                BankName = c.row.BankName,
                BankNumber = c.row.BankNumber,
                BankBranch = c.row.BankBranch,
                BankNote = c.row.BankNote,
                RoleId = c.r.Id,
                RoleName = c.r.Name,
                AccountTypeId = c.at.Id,
                AccountTypeName = c.at.Name,
                AccountStatusId = c.ast.Id,
                AccountStatusName = c.ast.Name,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<AccountViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<AccountViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        //created: JinDo
        //date: 15/12/2023
        //description: dangnhap
        public async Task<List<Account>> Login(Account obj)
        {
            if (db != null)
            {
                return await (
                    from a in db.Accounts
                    from at in db.AccountTypes
                    where (
                        a.Active == 1
                        //&& (a.Username == obj.Username || a.Email == obj.Username || a.Phone == obj.Phone)
                        && (a.Username == obj.Username || a.Email == obj.Username)
                        //&& (a.Username == obj.Username)
                        && a.AccountTypeId == at.Id
                    )
                    select a
                ).ToListAsync();
            }

            return null;
        }

        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await db.Accounts.Where(e => e.Email.Equals(email) && e.Active == 1).FirstOrDefaultAsync();
        }

        public async Task<Account> CheckCandidateByFacebookId(string facebookId)
        {
            return await db.Accounts.Where(x => x.Active == 1 && x.FacebookId == facebookId).FirstOrDefaultAsync();
        }

        public async Task<Account> CheckCandidateByAppleId(string appleId)
        {
            return await db.Accounts.Where(x => x.Active == 1 && x.AppleId == appleId).FirstOrDefaultAsync();
        }
        //end Dang Nhap

        /// <summary>
        /// Author: TrungHieuTr
        /// Description: đăng ký tài khoản
        /// </summary>
        /// <returns></returns>
        public async Task<Account> DetailByEmail(string? email)
        {
            if (db != null)
            {
                return await db.Accounts.AsNoTracking().FirstOrDefaultAsync(row =>
                    row.Active == 1 && row.Email.ToLower().Trim() == email.ToLower().Trim());
            }

            return null;
        }

        public async Task<bool> CheckUserNameExist(int id, string userName)
        {
            return await db.Accounts.AnyAsync(s => s.Active == 1 && s.Username == userName && s.Id != id);
        }

        public async Task<bool> EmailIsExisted(string email, int id)
        {
            return await db.Accounts.AnyAsync(c => c.Active == 1 && c.Email == email && c.Id != id);
        }

        public async Task<bool> PhoneIsExisted(string phone, int id)
        {
            return await db.Accounts.AnyAsync(c => c.Active == 1 && c.Phone == phone && c.Id != id);
        }

        public async Task<bool> CCCDIsExisted(string cccd, int id)
        {
            return await db.Accounts.AnyAsync(c => c.Active == 1 && c.IdCardNumber == cccd && c.Id != id);
        }

        // end đăng ký


        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Reset mật khẩu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task ResetNewPassword(Account obj)
        {
            var acc = await db.Accounts.FirstAsync(x => x.Id == obj.Id);
            //acc.Password = SecurityUtil.HashPassword(obj.Password.Trim());
            // thay doi 1 truong
            db.Attach(acc);
            db.Entry(acc).Property(s => s.Password).IsModified = true;
            //_context.Accounts.Update(acc); //khong duoc dung
            await db.SaveChangesAsync();
        }

        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await db.Accounts.AnyAsync(e => e.Email.Equals(email) && e.Active == 1);
        }

        public async Task<Account> FindByEmail(string email)
        {
            return await db.Accounts.FirstOrDefaultAsync(c => c.Email == email && c.Active == 1);
        }

        public async Task ChangePassword(Account obj)
        {
            if (db != null)
            {
                //Update that object
                db.Accounts.Attach(obj);
                db.Entry(obj).Property(x => x.Password).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateProfileAdmin(Account obj)
        {
            if (db != null)
            {
                //Update that object
                db.Accounts.Attach(obj);
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<Account> GetAccountById(int id)
        {
            var acc = await db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            return acc;
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<List<AccountAdminDTO>> DetailAccount(int? accountId)
        {
            if (db != null)
            {
                return await (
                        from row in db.Accounts
                        where row.Active == 1
                              && row.Id == accountId
                        select new AccountAdminDTO
                        {
                            Id = row.Id,
                            Active = row.Active,
                            RoleId = row.RoleId,
                            AccountStatusId = row.AccountStatusId,
                            AccountTypeId = row.AccountTypeId,
                            Phone = row.Phone,
                            Photo = row.Photo,
                            LastName = row.LastName,
                            MiddleName = row.MiddleName,
                            FirstName = row.FirstName,
                            Name = row.Name,
                            Email = row.Email,
                            Username = row.Username,
                            Password = row.Password,
                            Dob = row.Dob,
                            CreatedTime = row.CreatedTime,
                            Info = row.Info,
                            Address = row.Address,
                            IsActivated = row.IsActivated,
                            IdCardNumber = row.IdCardNumber,
                        })
                    .AsNoTracking().ToListAsync();
            }

            return null;
        }

        /// <summary>
        /// Author: HuyDQ
        /// Description: list danh sách theo roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<Account>> ListByRoleId(int roleId)
        {
            if (db != null)
            {
                return await (
                    from row in db.Accounts
                    where (row.Active == 1 && row.RoleId == roleId)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }

        public async Task UpdateAvatar(Account obj)
        {
            if (db != null)
            {
                //Update that object
                db.Accounts.Attach(obj);
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<ReponseContactDTO> ListContact(int accountId, int pageIndex, int pageSize)
        {
            if (db != null)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                var doctorList = await (from a in db.Accounts
                        join d in db.Doctor on a.Id equals d.AccountId
                        where (d.AccountId != accountId && d.Active == 1 && a.Active == 1)
                        select a)
                    .OrderByDescending(x => x.CreatedTime).ToListAsync();

                var userList = await db.Accounts
                    .Where(c => c.Id != accountId
                                && c.Active == 1).OrderByDescending(x => x.CreatedTime).ToListAsync();
                //var doctorPage = doctorList.Skip(offSet).Take(pageSize).ToList();
                userList = userList.Except(doctorList).ToList();

                var userPage = userList.Skip(offSet).Take(pageSize).ToList();

                userPage = userPage.Except(doctorList).ToList();

                var data = new ReponseContactDTO
                {
                    CountDoctor = doctorList.Count(),
                    CountUser = userList.Count(),
                    listDoctor = pageIndex != 0 && pageSize != 0 ? doctorList : doctorList,
                    listUser = pageIndex != 0 && pageSize != 0 ? userPage : userList
                };
                return data;
            }

            return null;
        }

        public async Task<AccountProfileDTO> Profile(int? id)
        {
            if (db != null)
            {
                var query = await (
                        from row in db.Accounts
                        join r in db.Roles on row.RoleId equals r.Id
                        where (row.Active == 1 && r.Active == 1 && row.Id == id)
                        select new AccountProfileDTO
                        {
                            Id = row.Id,
                            RoleId = r.Id,
                            RoleName = r.Name,
                            Name = row.Name,
                            FirstName = row.FirstName,
                            MiddleName = row.MiddleName,
                            LastName = row.LastName,
                            Address = row.Address,
                            AddressCity = row.AddressCity,
                            AddressDetail = row.AddressDetail,
                            AddressDistrict = row.AddressDistrict,
                            AddressWard = row.AddressWard,
                            Dob = row.Dob,
                            Description = row.Description,
                            Email = row.Email,
                            FacebookId = row.FacebookId,
                            Gender = row.Gender,
                            GoogleId = row.GoogleId,
                            IdCardGrantedDate = row.IdCardGrantedDate,
                            IdCardGrantedPlace = row.IdCardGrantedPlace,
                            IdCardNumber = row.IdCardNumber,
                            Info = row.Info,
                            Photo = row.Photo,
                            Phone = row.Phone,
                            AccountName = row.Username
                        })
                    .AsNoTracking().FirstOrDefaultAsync();
                if (query != null)
                {
                    if (query.AddressCity != null)
                    {
                        var ProvinceInfo = db.Provinces
                            .Where(x => x.Id == Int32.Parse(query.AddressCity) && x.Active == 1).Select(x => x.Name)
                            .FirstOrDefault();
                        query.AddressCityName = ProvinceInfo;
                    }

                    if (query.AddressDistrict != null)
                    {
                        var DistrictInfo = db.Districts
                            .Where(x => x.Id == Int32.Parse(query.AddressDistrict) && x.Active == 1).Select(x => x.Name)
                            .FirstOrDefault();
                        query.AddressDistrictName = DistrictInfo;
                    }

                    if (query.AddressWard != null)
                    {
                        var WardInfo = db.Wards.Where(x => x.Id == Int32.Parse(query.AddressWard) && x.Active == 1)
                            .Select(x => x.Name).FirstOrDefault();
                        query.AddressWardName = WardInfo;
                    }
                    ////Find Booking
                    //query.CountBookingWait = db.Bookings.Where(x => x.AccountId == query.Id && x.Active == 1 && (x.BookingStatusId == BookingStatusId.ACCEPT || x.BookingStatusId == BookingStatusId.WAIT)).Count();
                }

                return query;
            }

            return null;
        }

        public async Task SetDevice(Account obj)
        {
            if (db != null)
            {
                try
                {
                    //Update that object
                    db.Accounts.Attach(obj);
                    db.Entry(obj).Property(x => x.GuId).IsModified = true;
                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}