
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
    public class ServicesRepository : IServicesRepository
    {
        HomeDoctorContext db;
        public ServicesRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Service>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Services
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }

        public async Task<List<ServicesViewModel>> ListTest()
        {
            if (db != null)
            {
                return await (
                    from row in db.Services
                    from hf in db.HealthFacilities
                    where (row.Active == 1 && row.ParentId == SystemConstant.SERVICE_TEST_PARENT && row.HealthFacilityId == hf.Id && hf.Active == 1)
                    orderby row.Id descending
                    select new ServicesViewModel
                    {
                        Id = row.Id,
                        Active = row.Active,
                        Name = row.Name,
                        Description = row.Description,
                        Info = row.Info,
                        Price = row.Price,
                        HealthFacilityId = hf.Id,
                        HealthFacilityName = hf.Name,
                        CreatedTime = row.CreatedTime,
                        Photo = row.Photo
                    }
                ).ToListAsync();
            }
            return null;
        }
        public async Task<List<ServicesViewModel>> ListFourService()
        {
            if (db != null)
            {
                return await (
                    from row in db.Services
                    join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                    where (row.Active == 1 && hf.Active == 1)
                    orderby row.Id descending
                    select new ServicesViewModel
                    {
                        Id = row.Id,
                        Active = row.Active,
                        Name = row.Name,
                        Description = row.Description,
                        Info = row.Info,
                        Price = row.Price,
                        HealthFacilityId = hf.Id,
                        HealthFacilityName = hf.Name,
                        CreatedTime = row.CreatedTime,
                        Photo = row.Photo
                    }
                ).Take(4).ToListAsync();
            }
            return null;
        }

        public async Task<List<Service>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Services
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<Service>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Services
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }
        public async Task<List<Service>> ListPaging(int pageIndex, int pageSize, int clinicId) { 
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Services
                    where (row.Active == 1 && row.HealthFacilityId == clinicId)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }
        public async Task<List<ServicesViewModel>> ListPaging(int pageIndex, int pageSize, int clinicId,string keyword)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                var data = await (
                    from row in db.Services
                    join h in db.HealthFacilities on row.HealthFacilityId equals h.Id
                    where row.Active == 1
                    && h.Active == 1
                    && h.Id == row.HealthFacilityId
                    orderby row.CreatedTime descending
                    select new ServicesViewModel
                    {
                        Photo = row.Photo,
                        Id = row.Id,
                        HealthFacilityId = row.HealthFacilityId,
                        HealthFacilityName = h.Name,
                        CreatedTime = h.CreatedTime,
                        Description = row.Description,
                        Info = row.Info,
                        Price = row.Price,
                        Name = row.Name
                    }).ToListAsync();
                if(clinicId != 0)
                {
                    data = data.Where(x => x.HealthFacilityId == clinicId).ToList();
                }
                if(keyword != null)
                {
                    data = data.Where(x => x.Name.ToLower().Contains(keyword.ToLower())).ToList();
                }
                return data.Skip(offSet).Take(pageSize).ToList();
            }
            return null;
        }

        public async Task<Service> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Services.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }


        public async Task<Service> Add(Service obj)
        {
            if (db != null)
            {
                await db.Services.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(Service obj)
        {
            if (db != null)
            {
                //Update that object
                db.Services.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.Price).IsModified = true;
                db.Entry(obj).Property(x => x.HealthFacilityId).IsModified = true;
                db.Entry(obj).Property(x => x.ParentId).IsModified = true;
                db.Entry(obj).Property(x => x.Intro).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Service obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Services.Attach(obj);
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
                var obj = await db.Services.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Services.Remove(obj);

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
                    from row in db.Services
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<ServicesViewModel>> ListServerSide(ServicesDTParameters parameters)
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
            var query = from row in db.Services
                        join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                        join row2 in db.Services on row.ParentId equals row2.Id into ChildCategory
                        from cc in ChildCategory.DefaultIfEmpty()
                        where row.Active == 1 && hf.Active == 1
                        select new
                        {
                            row,
                            hf,
                            cc
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
EF.Functions.Collate(c.hf.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Price.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.CreatedTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) 
|| EF.Functions.Collate(c.cc.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General))

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
                        case "parentServiceName":
                            query = query.Where(c => (c.cc.Name ?? "").Contains(fillter));
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
                        case "price":
                            query = query.Where(c => c.row.Price.ToString().Trim().Contains(fillter));
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

            if (parameters.HealthFacilityIds.Count > 0)
            {
                query = query.Where(c => parameters.HealthFacilityIds.Contains(c.row.HealthFacility.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new ServicesViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                Price = c.row.Price,
                HealthFacilityId = c.hf.Id,
                HealthFacilityName = c.hf.Name,
                CreatedTime = c.row.CreatedTime,
                ParentServiceName = c.cc.Name

            });
            //4. Sort
            query2 = query2.OrderByDynamic<ServicesViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<ServicesViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<List<ServicesViewModel>> Top4Service(int id)
        {
           if(db != null)
            {
                var query = from row in db.Services
                            join h in db.HealthFacilities on row.HealthFacilityId equals h.Id
                            where row.Active == 1
                            && h.Active == 1
                            && h.Id == id
                            orderby row.CreatedTime descending
                            select new ServicesViewModel
                            {
                                Photo = row.Photo,
                                Id = row.Id,
                                HealthFacilityId = row.HealthFacilityId,
                                HealthFacilityName = h.Name,
                                CreatedTime = h.CreatedTime,
                                Description = row.Description,
                                Info = row.Info,
                                Price = row.Price,
                                Name = row.Name
                            };
                return await query.Take(4).ToListAsync();

            }
            return null;
        }
        public async Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Services
                    join h in db.HealthFacilities on row.HealthFacilityId equals h.Id
                    where row.Active == 1
                    && h.Active == 1
                    && h.Id == row.HealthFacilityId
                    orderby row.CreatedTime descending
                    select new ServicesViewModel
                    {
                        Photo = row.Photo,
                        Id = row.Id,
                        HealthFacilityId = row.HealthFacilityId,
                        HealthFacilityName = h.Name,
                        CreatedTime = h.CreatedTime,
                        Description = row.Description,
                        Info = row.Info,
                        Price = row.Price,
                        Name = row.Name
                    }).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }
        public async Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize, int clinicId)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Services
                    join h in db.HealthFacilities on row.HealthFacilityId equals h.Id
                    where row.Active == 1
                    && h.Active == 1
                    && h.Id == row.HealthFacilityId
                    && row.HealthFacilityId == clinicId
                    orderby row.CreatedTime descending
                    select new ServicesViewModel
                    {
                        Photo = row.Photo,
                        Id = row.Id,
                        HealthFacilityId = row.HealthFacilityId,
                        HealthFacilityName = h.Name,
                        CreatedTime = h.CreatedTime,
                        Description = row.Description,
                        Info = row.Info,
                        Price = row.Price,
                        Name = row.Name
                    }).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }
    }
}


