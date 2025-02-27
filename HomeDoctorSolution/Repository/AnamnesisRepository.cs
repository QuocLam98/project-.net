
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
    public class AnamnesisRepository : IAnamnesisRepository
    {
        HomeDoctorContext db;
        public AnamnesisRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Anamnesis>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Anamneses
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Anamnesis>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Anamneses
                    where (row.Active == 1 && (row.Name.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<Anamnesis>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Anamneses
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<Anamnesis> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Anamneses.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }


        public async Task<Anamnesis> Add(Anamnesis obj)
        {
            if (db != null)
            {
                await db.Anamneses.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(Anamnesis obj)
        {
            if (db != null)
            {
                //Update that object
                db.Anamneses.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = false;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.HeartDiseaseCard).IsModified = true;
                db.Entry(obj).Property(x => x.Diabetes).IsModified = true;
                db.Entry(obj).Property(x => x.Asthma).IsModified = true;
                db.Entry(obj).Property(x => x.Epilepsy).IsModified = true;
                db.Entry(obj).Property(x => x.Depression).IsModified = true;
                db.Entry(obj).Property(x => x.Stress).IsModified = true;
                db.Entry(obj).Property(x => x.AnxietyDisorders).IsModified = true;
                db.Entry(obj).Property(x => x.Orther).IsModified = true;
                //db.Entry(obj).Property(x => x.MedicalProfileId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Anamnesis obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Anamneses.Attach(obj);
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
                var obj = await db.Anamneses.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Anamneses.Remove(obj);

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
                    from row in db.Anamneses
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<AnamnesisViewModel>> ListServerSide(AnamnesisDTParameters parameters)
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
            var query = from row in db.Anamneses

                        //join mp in db.MedicalProfiles on row.MedicalProfileId equals mp.Id

                        where row.Active == 1
                                        //&& mp.Active == 1

                        select new
                        {
                            row,
                            //mp
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
EF.Functions.Collate(c.row.HeartDiseaseCard.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Diabetes.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Asthma.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Epilepsy.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Depression.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Stress.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.AnxietyDisorders.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Orther.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                        case "heartDiseaseCard":
                            query = query.Where(c => c.row.HeartDiseaseCard.ToString().Trim().Contains(fillter));
                            break;
                        case "diabetes":
                            query = query.Where(c => c.row.Diabetes.ToString().Trim().Contains(fillter));
                            break;
                        case "asthma":
                            query = query.Where(c => c.row.Asthma.ToString().Trim().Contains(fillter));
                            break;
                        case "epilepsy":
                            query = query.Where(c => c.row.Epilepsy.ToString().Trim().Contains(fillter));
                            break;
                        case "depression":
                            query = query.Where(c => c.row.Depression.ToString().Trim().Contains(fillter));
                            break;
                        case "stress":
                            query = query.Where(c => c.row.Stress.ToString().Trim().Contains(fillter));
                            break;
                        case "anxietyDisorders":
                            query = query.Where(c => c.row.AnxietyDisorders.ToString().Trim().Contains(fillter));
                            break;
                        case "orther":
                            query = query.Where(c => (c.row.Orther ?? "").Contains(fillter));
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

            //if (parameters.MedicalProfileIds.Count > 0)
            //{
            //    query = query.Where(c => parameters.MedicalProfileIds.Contains(c.row.MedicalProfile.Id));
            //}


            //3.Query second
            var query2 = query.Select(c => new AnamnesisViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                HeartDiseaseCard = c.row.HeartDiseaseCard,
                Diabetes = c.row.Diabetes,
                Asthma = c.row.Asthma,
                Epilepsy = c.row.Epilepsy,
                Depression = c.row.Depression,
                Stress = c.row.Stress,
                AnxietyDisorders = c.row.AnxietyDisorders,
                Orther = c.row.Orther,
                //MedicalProfileId = c.mp.Id,
                //MedicalProfileName = c.mp.Name,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<AnamnesisViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<AnamnesisViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }
        public async Task<bool> CheckExistByAccountId(int? accountId)
        {
            if (db != null)
            {
                var result = await db.Anamneses.AnyAsync(x => x.AccountId == accountId && x.Active == 1);
                return result;
            }
            return false;
        }
        public async Task<List<Anamnesis>> DetailByAccountId(int? accountId)
        {
            if (db != null)
            {
                var result = await db.Anamneses.Where(x => x.AccountId == accountId && x.Active == 1).AsNoTracking().ToListAsync();
                return result;
            }
            return null;
        }
    }
}


