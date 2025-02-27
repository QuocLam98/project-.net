
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

namespace HomeDoctorSolution.Repository
{
    public class MedicalProfileRepository : IMedicalProfileRepository
    {
        HomeDoctorContext db;
        public MedicalProfileRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<MedicalProfile>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.MedicalProfiles
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<MedicalProfile>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.MedicalProfiles
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<MedicalProfile>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.MedicalProfiles
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<MedicalProfile> Detail(int? id)
        {
            if (db != null)
            {
                return await db.MedicalProfiles.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }


        public async Task<MedicalProfile> Add(MedicalProfile obj)
        {
            if (db != null)
            {
                await db.MedicalProfiles.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(MedicalProfile obj)
        {
            if (db != null)
            {
                //Update that object
                db.MedicalProfiles.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = false;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.Diagnose).IsModified = true;
                db.Entry(obj).Property(x => x.HealthInfo).IsModified = true;
                db.Entry(obj).Property(x => x.Height).IsModified = true;
                db.Entry(obj).Property(x => x.Weight).IsModified = true;
                db.Entry(obj).Property(x => x.Bmi).IsModified = true;
                db.Entry(obj).Property(x => x.GradeId).IsModified = true;
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(MedicalProfile obj)
        {
            if (db != null)
            {
                //Update that obj
                db.MedicalProfiles.Attach(obj);
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
                var obj = await db.MedicalProfiles.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.MedicalProfiles.Remove(obj);

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
                    from row in db.MedicalProfiles
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<MedicalProfileViewModel>> ListServerSide(MedicalProfileDTParameters parameters)
        {
            //0. Options
            string searchAll = parameters.SearchAll.Trim();//Trim text
            string orderCritirea = "Id";//Set default critirea
            int recordTotal, recordFiltered;
            bool orderDirectionASC = true;//Set default ascending
            if (parameters.Order != null)
            {
                orderCritirea = parameters.Columns[parameters.Order[0].Column].Data;
                orderDirectionASC = parameters.Order[0].Dir == DTOrderDir.ASC;
            }
            //1. Join
            var query = from row in db.MedicalProfiles

                        join a in db.Accounts on row.AccountId equals a.Id

                        where row.Active == 1
                                        && a.Active == 1

                        select new
                        {
                            row,
                            a
                        };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Diagnose.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.HealthInfo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Height.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Weight.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Bmi.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.GradeId.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.CreatedTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General))

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
                        case "name":
                            query = query.Where(c => (c.row.Name ?? "").Contains(fillter));
                            break;
                        case "description":
                            query = query.Where(c => (c.row.Description ?? "").Contains(fillter));
                            break;
                        case "info":
                            query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
                            break;
                        case "diagnose":
                            query = query.Where(c => (c.row.Diagnose ?? "").Contains(fillter));
                            break;
                        case "healthInfo":
                            query = query.Where(c => (c.row.HealthInfo ?? "").Contains(fillter));
                            break;
                        case "height":
                            query = query.Where(c => (c.row.Height ?? "").Contains(fillter));
                            break;
                        case "weight":
                            query = query.Where(c => (c.row.Weight ?? "").Contains(fillter));
                            break;
                        case "bMI":
                            query = query.Where(c => (c.row.Bmi ?? "").Contains(fillter));
                            break;
                        case "gradeId":
                            query = query.Where(c => c.row.GradeId.ToString().Trim().Contains(fillter));
                            break;
                        case "createdTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.CreatedTime >= startDate && c.row.CreatedTime <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.CreatedTime.Date == date.Date);
                            }
                            break;

                    }
                }
            }

            if (parameters.AccountIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountIds.Contains(c.row.Account.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new MedicalProfileViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                Diagnose = c.row.Diagnose,
                HealthInfo = c.row.HealthInfo,
                Height = c.row.Height,
                Weight = c.row.Weight,
                Bmi = c.row.Bmi,
                GradeId = c.row.GradeId,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<MedicalProfileViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<MedicalProfileViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> CheckExistByAccountId(int? accountId)
        {
            if (db != null)
            {
                var result = await db.MedicalProfiles.AnyAsync(x => x.AccountId == accountId && x.Active == 1);
                return result;
            }

            return false;
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<List<MedicalProfile>> DetailByAccountId(int? accountId)
        {
            if (db != null)
            {
                return await db.MedicalProfiles.Where(x => x.Active == 1 && x.AccountId == accountId).AsNoTracking().ToListAsync();
            }

            return null;
        }
        public async Task<List<MedicalProfileViewModel>> MedicalProfile(int? accountId)
        {
            if(db != null)
            {
                return await (
                    from mp in db.MedicalProfiles
                    join a in db.Accounts on mp.AccountId equals a.Id
                    join an in db.Anamneses on a.Id equals an.AccountId into anGroup
                    from an in anGroup.DefaultIfEmpty()
                        //join gr in db.Grades on mp.GradeId equals gr.Id
                    where (mp.Active == 1 && a.Id == accountId)
                    orderby mp.Id descending
                    select new MedicalProfileViewModel()
                    {
                        Id = mp.Id,
                        Name = mp.Name,
                        Phone = a.Phone,
                        HealthInfo = mp.HealthInfo,
                        Height = mp.Height,
                        Weight = mp.Weight,
                        Bmi = mp.Bmi,
                        GradeId = mp.GradeId,
                        Diagnose = mp.Diagnose,
                        HeartDiseaseCard = an.HeartDiseaseCard,
                        Diabetes = an.Diabetes,
                        Asthma = an.Asthma,
                        Epilepsy = an.Epilepsy,
                        Depression = an.Depression,
                        Stress = an.Stress,
                        AnxietyDisorders = an.AnxietyDisorders,
                        Orther = an.Orther,
                        CreatedTime = mp.CreatedTime,
                    }
                ).ToListAsync();
            }
            return null;
        }
    }
}


