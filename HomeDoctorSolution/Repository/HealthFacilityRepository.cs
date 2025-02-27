
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
    public class HealthFacilityRepository : IHealthFacilityRepository
    {
        HomeDoctorContext db;
        public HealthFacilityRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<HealthFacility>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.HealthFacilities
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<HealthFacility>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.HealthFacilities
                    where (row.Active == 1 && (row.Name.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<HealthFacility>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.HealthFacilities
                    where (row.Active == 1)
                    orderby row.Id descending
                    select new HealthFacility()
                    {
                        Id = row.Id,
                        Active = row.Active,
                        Name = row.Name,
                        DistrictId = row.DistrictId,
                        Email = row.Email,
                        FoundedYear = row.FoundedYear,
                        HealthFacilityStatusId = row.HealthFacilityStatusId,
                        HealthFacilityTypeId = row.HealthFacilityTypeId,
                        Info = row.Info,
                        Linkmap = row.Linkmap,
                        Phone = row.Phone,
                        Photo = row.Photo,
                        OpenDate = row.OpenDate,
                        AddressDetail = row.AddressDetail,
                        HealthFacilityStatus = db.HealthFacilityStatuses.Where(x => x.Id == row.HealthFacilityStatusId).FirstOrDefault(),
                        HealthFacilityType = db.HealthFacilityTypes.Where(x => x.Id == row.HealthFacilityTypeId).FirstOrDefault(),
                    }
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }

        public async Task<List<HealthFacility>> ListPagingView(int pageIndex, int pageSize, int provinceId, string keyword)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.HealthFacilities
                    from dis in db.Districts
                    from pro in db.Provinces
                    where (row.Active == 1 && (provinceId != 0 ? (row.DistrictId == dis.Id && dis.Active == 1 && pro.Id == dis.ProvinceId && pro.Id == provinceId && pro.Active == 1) : (pro.Active == 1 && dis.Active == 1)) && row.Name.Trim().ToLower().Contains(keyword.Trim().ToLower()))
                    orderby row.Id descending
                    select new HealthFacility()
                    {
                        Id = row.Id,
                        Active = row.Active,
                        Name = row.Name,
                        DistrictId = row.DistrictId,
                        Email = row.Email,
                        FoundedYear = row.FoundedYear,
                        HealthFacilityStatusId = row.HealthFacilityStatusId,
                        HealthFacilityTypeId = row.HealthFacilityTypeId,
                        Info = row.Info,
                        Linkmap = row.Linkmap,
                        Phone = row.Phone,
                        Photo = row.Photo,
                        OpenDate = row.OpenDate,
                        AddressDetail = row.AddressDetail,
                        HealthFacilityStatus = db.HealthFacilityStatuses.Where(x => x.Id == row.HealthFacilityStatusId).FirstOrDefault(),
                        HealthFacilityType = db.HealthFacilityTypes.Where(x => x.Id == row.HealthFacilityTypeId).FirstOrDefault(),
                    }
                ).Distinct().Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<HealthFacility> Detail(int? id)
        {
            if (db != null)
            {
                return await db.HealthFacilities.FirstOrDefaultAsync(x => x.Id == id && x.Active == 1);
            }

            return null;
        }


        public async Task<HealthFacility> Add(HealthFacility obj)
        {
            if (db != null)
            {
                await db.HealthFacilities.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(HealthFacility obj)
        {
            if (db != null)
            {
                //Update that object
                db.HealthFacilities.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Phone).IsModified = true;
                db.Entry(obj).Property(x => x.Email).IsModified = true;
                db.Entry(obj).Property(x => x.FoundedYear).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.OpenDate).IsModified = true;
                db.Entry(obj).Property(x => x.DistrictId).IsModified = true;
                db.Entry(obj).Property(x => x.Linkmap).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.HealthFacilityTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.HealthFacilityStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.ProvinceId).IsModified = true;


                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(HealthFacility obj)
        {
            if (db != null)
            {
                //Update that obj
                db.HealthFacilities.Attach(obj);
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
                var obj = await db.HealthFacilities.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.HealthFacilities.Remove(obj);

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
                    from row in db.HealthFacilities
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<HealthFacilityViewModel>> ListServerSide(HealthFacilityDTParameters parameters)
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
            var query = from row in db.HealthFacilities

                        join hft in db.HealthFacilityTypes on row.HealthFacilityTypeId equals hft.Id
                        join hfs in db.HealthFacilityStatuses on row.HealthFacilityStatusId equals hfs.Id

                        where row.Active == 1
                                        && hft.Active == 1
    && hfs.Active == 1

                        select new
                        {
                            row,
                            hft,
                            hfs
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
EF.Functions.Collate(c.row.Phone.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Email.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.FoundedYear.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.DistrictId.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Linkmap.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                        case "phone":
                            query = query.Where(c => (c.row.Phone ?? "").Contains(fillter));
                            break;
                        case "email":
                            query = query.Where(c => (c.row.Email ?? "").Contains(fillter));
                            break;
                        case "foundedYear":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.FoundedYear >= startDate && c.row.FoundedYear <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.FoundedYear.Date == date.Date);
                            }
                            break;
                        case "districtId":
                            query = query.Where(c => c.row.DistrictId.ToString().Trim().Contains(fillter));
                            break;
                        case "linkmap":
                            query = query.Where(c => (c.row.Linkmap ?? "").Contains(fillter));
                            break;
                        case "info":
                            query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
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

            if (parameters.HealthFacilityTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.HealthFacilityTypeIds.Contains(c.row.HealthFacilityType.Id));
            }


            if (parameters.HealthFacilityStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.HealthFacilityStatusIds.Contains(c.row.HealthFacilityStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new HealthFacilityViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Phone = c.row.Phone,
                Email = c.row.Email,
                FoundedYear = c.row.FoundedYear,
                DistrictId = c.row.DistrictId,
                Linkmap = c.row.Linkmap,
                Info = c.row.Info,
                HealthFacilityTypeId = c.hft.Id,
                HealthFacilityTypeName = c.hft.Name,
                HealthFacilityStatusId = c.hfs.Id,
                HealthFacilityStatusName = c.hfs.Name,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<HealthFacilityViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<HealthFacilityViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }
    }
}


