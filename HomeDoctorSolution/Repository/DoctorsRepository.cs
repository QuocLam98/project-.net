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
    public class DoctorsRepository : IDoctorsRepository
    {
        HomeDoctorContext db;

        public DoctorsRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Doctor>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Doctor>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Doctor>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }

        public async Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    join a in db.Accounts on row.AccountId equals a.Id
                    join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                    join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                    join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                    join se in db.Services on row.ServicesId equals se.Id
                    where row.Active == 1
                          && a.Active == 1
                          && dt.Active == 1
                          && ds.Active == 1
                          && hf.Active == 1
                          && se.Active == 1
                    orderby row.CreatedTime descending
                    select new DoctorsViewModel
                    {
                        Id = row.Id,
                        Address = row.Address,
                        Name = row.Name,
                        Image = a.Photo,
                        Description = row.Description,
                        Info = row.Info,
                        WorkAddress = row.WorkAddress,
                        Position = row.Position,
                        Experience = row.Experience,
                        Specialist = row.Specialist,
                        Language = row.Language,
                        License = row.License,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        DoctorTypeId = dt.Id,
                        DoctorTypeName = dt.Name,
                        DoctorStatusId = ds.Id,
                        DoctorStatusName = ds.Name,
                        HealthFacilityId = hf.Id,
                        HealthFacilityName = hf.Name,
                        CreatedTime = row.CreatedTime,
                        ServicesId = row.ServicesId,
                        ServiceName = se.Name
                    }
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }

        //List doctor with specific service and healt facility
        public async Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId,
            int healthFacilityId)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    join a in db.Accounts on row.AccountId equals a.Id
                    join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                    join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                    join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                    join se in db.Services on row.ServicesId equals se.Id
                    where row.Active == 1
                          && a.Active == 1
                          && dt.Active == 1
                          && ds.Active == 1
                          && hf.Active == 1
                          && se.Active == 1
                          && row.ServicesId == serviceId
                          && row.HealthFacilityId == healthFacilityId
                    orderby row.CreatedTime descending
                    select new DoctorsViewModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Image = a.Photo,
                        Description = row.Description,
                        Info = row.Info,
                        WorkAddress = row.WorkAddress,
                        Position = row.Position,
                        Experience = row.Experience,
                        Specialist = row.Specialist,
                        Language = row.Language,
                        License = row.License,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        DoctorTypeId = dt.Id,
                        DoctorTypeName = dt.Name,
                        DoctorStatusId = ds.Id,
                        DoctorStatusName = ds.Name,
                        HealthFacilityId = hf.Id,
                        HealthFacilityName = hf.Name,
                        HealthFacilityAddress = hf.AddressDetail,
                        CreatedTime = row.CreatedTime,
                        ServicesId = row.ServicesId,
                        ServiceName = se.Name
                    }
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }

        public async Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId,
            int healthFacilityId, string keyword, string district)
        {
            try
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;

                if (db != null)
                {
                    var data = await (
                        from row in db.Doctor
                        join a in db.Accounts on row.AccountId equals a.Id
                        join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                        join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                        join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                        join se in db.Services on row.ServicesId equals se.Id
                        where row.Active == 1
                              && a.Active == 1
                              && dt.Active == 1
                              && ds.Active == 1
                              && se.Active == 1
                              && (healthFacilityId != 0 ? hf.Id == healthFacilityId && hf.Active == 1 : hf.Active == 1)
                        orderby row.CreatedTime descending
                        select new DoctorsViewModel
                        {
                            Id = row.Id,
                            Address = row.Address,
                            Name = row.Name,
                            Image = a.Photo,
                            Description = row.Description,
                            Info = row.Info,
                            WorkAddress = row.WorkAddress,
                            Position = row.Position,
                            Experience = row.Experience,
                            Specialist = row.Specialist,
                            Language = row.Language,
                            License = row.License,
                            AccountId = a.Id,
                            AccountName = a.Name,
                            DoctorTypeId = dt.Id,
                            DoctorTypeName = dt.Name,
                            DoctorStatusId = ds.Id,
                            DoctorStatusName = ds.Name,
                            HealthFacilityId = hf.Id,
                            HealthFacilityName = hf.Name,
                            HealthFacilityAddress = hf.AddressDetail,
                            CreatedTime = row.CreatedTime,
                            ServicesId = row.ServicesId,
                            ServiceName = se.Name
                        }
                    ).ToListAsync();

                    if (healthFacilityId != 0)
                    {
                        data = data.Where(x => x.HealthFacilityId == healthFacilityId).ToList();
                    }

                    if (serviceId != 0)
                    {
                        data = data.Where(x => x.ServicesId == serviceId).ToList();
                    }

                    if (keyword != null)
                    {
                        data = data.Where(x => x.Name.ToLower().Contains(keyword.ToLower())).ToList();
                    }

                    if (district != null && district != "null")
                    {
                        data = data.Where(x => x.Address.ToLower().Contains(district.ToLower()) || x.Address == "1000")
                            .ToList();
                    }


                    return data.Skip(offSet).Take(pageSize).ToList();
                }
                else
                {
                    return null; // Trả về null nếu db là null
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: log, thông báo người dùng, ...
                // Có thể trả về một danh sách trống hoặc null nếu không muốn truyền ngoại lệ ra bên ngoài phương thức
                // Log ex.Message hoặc ex.StackTrace để debug
                return null;
            }
        }

        public async Task<int> listPagingViewModelCount(int serviceId,
            int healthFacilityId, string keyword, string district)
        {
            try
            {
                if (db != null)
                {
                    var data = await (
                        from row in db.Doctor
                        join a in db.Accounts on row.AccountId equals a.Id
                        join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                        join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                        join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                        join se in db.Services on row.ServicesId equals se.Id
                        where row.Active == 1
                              && a.Active == 1
                              && dt.Active == 1
                              && ds.Active == 1
                              && se.Active == 1
                              && (healthFacilityId != 0 ? hf.Id == healthFacilityId && hf.Active == 1 : hf.Active == 1)
                        orderby row.CreatedTime descending
                        select new DoctorsViewModel
                        {
                            Id = row.Id,
                            Address = row.Address,
                            Name = row.Name,
                            Image = a.Photo,
                            Description = row.Description,
                            Info = row.Info,
                            WorkAddress = row.WorkAddress,
                            Position = row.Position,
                            Experience = row.Experience,
                            Specialist = row.Specialist,
                            Language = row.Language,
                            License = row.License,
                            AccountId = a.Id,
                            AccountName = a.Name,
                            DoctorTypeId = dt.Id,
                            DoctorTypeName = dt.Name,
                            DoctorStatusId = ds.Id,
                            DoctorStatusName = ds.Name,
                            HealthFacilityId = hf.Id,
                            HealthFacilityName = hf.Name,
                            HealthFacilityAddress = hf.AddressDetail,
                            CreatedTime = row.CreatedTime,
                            ServicesId = row.ServicesId,
                            ServiceName = se.Name
                        }
                    ).ToListAsync();

                    if (healthFacilityId != 0)
                    {
                        data = data.Where(x => x.HealthFacilityId == healthFacilityId).ToList();
                    }

                    if (serviceId != 0)
                    {
                        data = data.Where(x => x.ServicesId == serviceId).ToList();
                    }

                    if (keyword != null)
                    {
                        data = data.Where(x => x.Name.ToLower().Contains(keyword.ToLower())).ToList();
                    }

                    if (district != null && district != "null")
                    {
                        data = data.Where(x => x.Address.ToLower().Contains(district.ToLower()) || x.Address == "1000")
                            .ToList();
                    }


                    return data.Count();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<Doctor> DetailViewModel(int? id)
        {
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    join a in db.Accounts on row.AccountId equals a.Id
                    join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                    join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                    join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                    join s in db.Services on row.ServicesId equals s.Id
                    where row.Active == 1
                          && a.Active == 1
                          && dt.Active == 1
                          && ds.Active == 1
                          && hf.Active == 1
                          && row.Id == id
                    orderby row.CreatedTime descending
                    select new DoctorsViewModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Image = a.Photo,
                        Description = row.Description,
                        Info = row.Info,
                        WorkAddress = row.WorkAddress,
                        Position = row.Position,
                        Experience = row.Experience,
                        Specialist = row.Specialist,
                        Language = row.Language,
                        License = s.Name,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        DoctorTypeId = dt.Id,
                        DoctorTypeName = dt.Name,
                        DoctorStatusId = ds.Id,
                        DoctorStatusName = ds.Name,
                        HealthFacilityId = hf.Id,
                        HealthFacilityName = hf.Name,
                        CreatedTime = row.CreatedTime,
                        HealthFacilityAddress = hf.AddressDetail,
                        ServiceFee = s.Price,
                        ServiceName = s.Name,
                        ServicesId = s.Id,
                        StartTime = row.StartTime,
                        EndTime = row.EndTime
                    }
                ).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<Doctor> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Doctor.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }

            return null;
        }


        public async Task<Doctor> Add(Doctor obj)
        {
            if (db != null)
            {
                await db.Doctor.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }

            return null;
        }


        public async Task Update(Doctor obj)
        {
            if (db != null)
            {
                //Update that object
                db.Doctor.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.WorkAddress).IsModified = true;
                db.Entry(obj).Property(x => x.Address).IsModified = true;
                db.Entry(obj).Property(x => x.Position).IsModified = true;
                db.Entry(obj).Property(x => x.Experience).IsModified = true;
                db.Entry(obj).Property(x => x.Specialist).IsModified = true;
                db.Entry(obj).Property(x => x.Language).IsModified = true;
                db.Entry(obj).Property(x => x.License).IsModified = true;
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;
                db.Entry(obj).Property(x => x.DoctorTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.DoctorStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.ServicesId).IsModified = true;
                db.Entry(obj).Property(x => x.HealthFacilityId).IsModified = true;
                db.Entry(obj).Property(x => x.StartTime).IsModified = true;
                db.Entry(obj).Property(x => x.EndTime).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Doctor obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Doctor.Attach(obj);
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
                var obj = await db.Doctor.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Doctor.Remove(obj);

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
                    from row in db.Doctor
                    where row.Active == 1
                    select row
                ).Count();
            }

            return result;
        }

        public async Task<DTResult<DoctorsViewModel>> ListServerSide(DoctorsDTParameters parameters)
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
            var query = from row in db.Doctor
                join a in db.Accounts on row.AccountId equals a.Id
                join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                where row.Active == 1
                      && a.Active == 1
                      && dt.Active == 1
                      && ds.Active == 1
                      && hf.Active == 1
                select new
                {
                    row,
                    a,
                    dt,
                    ds,
                    hf
                };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.WorkAddress.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Position.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Experience.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Specialist.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Language.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.License.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.CreatedTime.ToCustomString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General))
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
                        case "workAddress":
                            query = query.Where(c => (c.row.WorkAddress ?? "").Contains(fillter));
                            break;
                        case "position":
                            query = query.Where(c => (c.row.Position ?? "").Contains(fillter));
                            break;
                        case "experience":
                            query = query.Where(c => (c.row.Experience ?? "").Contains(fillter));
                            break;
                        case "specialist":
                            query = query.Where(c => (c.row.Specialist ?? "").Contains(fillter));
                            break;
                        case "language":
                            query = query.Where(c => (c.row.Language ?? "").Contains(fillter));
                            break;
                        case "license":
                            query = query.Where(c => (c.row.License ?? "").Contains(fillter));
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
                    }
                }
            }

            if (parameters.AccountIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountIds.Contains(c.row.Account.Id));
            }


            if (parameters.DoctorTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.DoctorTypeIds.Contains(c.row.DoctorType.Id));
            }


            if (parameters.DoctorStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.DoctorStatusIds.Contains(c.row.DoctorStatus.Id));
            }


            if (parameters.HealthFacilityIds.Count > 0)
            {
                query = query.Where(c => parameters.HealthFacilityIds.Contains(c.row.HealthFacility.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new DoctorsViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                WorkAddress = c.row.WorkAddress,
                Position = c.row.Position,
                Experience = c.row.Experience,
                Specialist = c.row.Specialist,
                Language = c.row.Language,
                License = c.row.License,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                DoctorTypeId = c.dt.Id,
                DoctorTypeName = c.dt.Name,
                DoctorStatusId = c.ds.Id,
                DoctorStatusName = c.ds.Name,
                HealthFacilityId = c.hf.Id,
                HealthFacilityName = c.hf.Name,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<DoctorsViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<DoctorsViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<List<Doctors>> ListDoctor()
        {
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    join a in db.Accounts on row.AccountId equals a.Id
                    join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                    join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                    join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                    where (row.Active == 1
                           && a.Active == 1
                           && dt.Active == 1
                           && ds.Active == 1
                           && hf.Active == 1
                           && row.DoctorStatusId == SystemConstant.DOCTOR_STATUS_ACTIVE)
                    orderby row.Id descending
                    select new Doctors
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Active = row.Active,
                        Description = row.Description,
                        Info = row.Info,
                        WorkAddress = row.WorkAddress,
                        Position = row.Position,
                        Experience = row.Experience,
                        Specialist = row.Specialist,
                        Language = row.Language,
                        License = row.License,
                        AccountId = row.AccountId,
                        DoctorTypeId = row.DoctorTypeId,
                        DoctorStatusId = row.DoctorStatusId,
                        HealthFacilityId = row.HealthFacilityId,
                        CreatedTime = row.CreatedTime,
                        Account = a.Name,
                        DoctorType = dt.Name,
                        DoctorStatus = ds.Name,
                        HealthFacility = hf.Name
                    }
                ).ToListAsync();
            }

            return null;
        }

        public async Task<List<DoctorsViewModel>> ListThreeDoctorByTimeDesc()
        {
            if (db != null)
            {
                return await (
                    from row in db.Doctor
                    join a in db.Accounts on row.AccountId equals a.Id
                    join dt in db.DoctorTypes on row.DoctorTypeId equals dt.Id
                    join ds in db.DoctorStatuses on row.DoctorStatusId equals ds.Id
                    join hf in db.HealthFacilities on row.HealthFacilityId equals hf.Id
                    where row.Active == 1
                          && a.Active == 1
                          && dt.Active == 1
                          && ds.Active == 1
                          && hf.Active == 1
                    orderby row.CreatedTime descending
                    select new DoctorsViewModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Image = a.Photo,
                        Description = row.Description,
                        Info = row.Info,
                        WorkAddress = row.WorkAddress,
                        Position = row.Position,
                        Experience = row.Experience,
                        Specialist = row.Specialist,
                        Language = row.Language,
                        License = row.License,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        DoctorTypeId = dt.Id,
                        DoctorTypeName = dt.Name,
                        DoctorStatusId = ds.Id,
                        DoctorStatusName = ds.Name,
                        HealthFacilityId = hf.Id,
                        HealthFacilityName = hf.Name,
                        CreatedTime = row.CreatedTime,
                    }
                ).Take(3).ToListAsync();
            }

            return null;
        }

        public async Task<List<Doctor>> get6DoctorOutstanding()
        {
            if (db != null)
            {
                List<Doctor> listOutStandingDoctor = new List<Doctor>();
                var getCounselorId = await db.Bookings
                    .Where(x => x.Active == 1)
                    .GroupBy(row => row.CounselorId)
                    .Select(group => new Booking
                    {
                        CounselorId = group.Key,
                        //Lưu tạm vào accountId số lượng booking
                        AccountId = group.Count()
                    })
                    .OrderByDescending(item => item.AccountId)
                    .Take(5).ToListAsync();
                ;
                for (var i = 0; i < getCounselorId.Count; i++)
                {
                    var getDoc = await DetailViewModel(getCounselorId[i].CounselorId);
                    listOutStandingDoctor.Add(getDoc);
                }

                return listOutStandingDoctor;
            }

            return null;
        }

        public async Task<List<DoctorsViewModel>> ListDoctorBooking(int AccountId)
        {
            if (db != null)
            {
                var doctorsList = await (
                    from row in db.Doctor
                    join b in db.Bookings on row.Id equals b.CounselorId
                    join a in db.Accounts on row.AccountId equals a.Id
                    join d in db.HealthFacilities on row.HealthFacilityId equals d.Id
                    join s in db.Services on row.ServicesId equals s.Id
                    where row.Active == 1 && b.Active == 1 && b.AccountId == AccountId && d.Active == 1
                    group new { row, a, d, s, b } by new { row.Id, row.Name }
                    into grouped
                    orderby grouped.Max(g => g.b.Id) descending
                    select new DoctorsViewModel
                    {
                        BookingId = grouped.Max(g => g.b.Id),
                        Id = grouped.Key.Id,
                        Name = grouped.Key.Name,
                        Active = grouped.Max(g => g.row.Active),
                        Description = grouped.Max(g => g.row.Description),
                        Info = grouped.Max(g => g.row.Info),
                        WorkAddress = grouped.Max(g => g.row.WorkAddress),
                        Position = grouped.Max(g => g.row.Position),
                        Experience = grouped.Max(g => g.row.Experience),
                        Specialist = grouped.Max(g => g.row.Specialist),
                        Language = grouped.Max(g => g.row.Language),
                        License = grouped.Max(g => g.row.License),
                        AccountId = grouped.Max(g => g.row.AccountId),
                        DoctorTypeId = grouped.Max(g => g.row.DoctorTypeId),
                        DoctorStatusId = grouped.Max(g => g.row.DoctorStatusId),
                        HealthFacilityId = grouped.Max(g => g.row.HealthFacilityId),
                        CreatedTime = grouped.Max(g => g.row.CreatedTime),
                        AccountName = grouped.Max(g => g.a.Name),
                        HealthFacilityName = grouped.Max(g => g.d.Name),
                        Image = grouped.Max(g => g.a.Photo),
                        ServiceName = grouped.Max(g => g.s.Name),
                        ServicesId = grouped.Max(g => g.s.Id),
                    }
                ).Take(3).ToListAsync();

                return doctorsList;
            }

            return null;
        }
    }
}