
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
using HomeDoctorSolution.Constants;
using NPOI.SS.Formula.Functions;

namespace HomeDoctorSolution.Repository
{
    public class BookingRepository : IBookingRepository
    {
        HomeDoctorContext db;
        public BookingRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Booking>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Bookings
                    from service in db.Services
                    where (row.Active == 1 && service.Active == 1 && row.ServiceId == service.Id && service.ParentId != SystemConstant.SERVICE_TEST_PARENT)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }

        public async Task<List<Booking>> ListTest()
        {
            if (db != null)
            {
                return await (
                    from row in db.Bookings
                    from service in db.Services
                    where (row.Active == 1 && row.ServiceId == service.Id && service.Active == 1 && service.ParentId == SystemConstant.SERVICE_TEST_PARENT)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }
        public async Task<List<Booking>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Bookings
                    where (row.Active == 1 && (row.Name.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<Booking>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Bookings
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<Booking> Detail(int? id)
        {
            if (db != null)
            {
                var detail = await db.Bookings.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
                if (detail != null)
                {
                    detail.Consultant = await db.Consultants.Where(x => x.BookingId == detail.Id).OrderBy(x => x.Id).ToListAsync();
                }
                return detail;
            }
            return null;
        }

        public async Task<BookingViewModel> DetailViewModel(int id)
        {
            var result = await (
                            from b in db.Bookings
                            join a in db.Accounts on b.AccountId equals a.Id
                            join bs in db.BookingStatuses on b.BookingStatusId equals bs.Id
                            join bt in db.BookingTypes on b.BookingTypeId equals bt.Id
                            join dt in db.Doctor on b.CounselorId equals dt.Id
                            join sv in db.Services on dt.ServicesId equals sv.Id
                            where a.Active == 1 && a.IsActivated == 1
                            && b.Active == 1
                            && bt.Active == 1
                            && bs.Active == 1
                            && dt.Active == 1
                            && sv.Active == 1
                            && b.Id == id
                            select new BookingViewModel()
                            {
                                Id = b.Id,
                                BookingTypeId = b.BookingTypeId,
                                BookingTypeName = bt.Name,
                                BookingStatusId = b.BookingStatusId,
                                BookingStatusName = bs.Name,
                                AccountId = b.AccountId,
                                AccountName = a.Name,
                                CounselorId = (int)b.CounselorId,
                                CounselorName = dt.Name,
                                Photo = "",
                                ServiceName = sv.Name,
                                StartTime = b.StartTime,
                                EndTime = b.EndTime,
                                Info = b.Info,
                                CreatedTime = b.CreatedTime,
                                Price = sv.Price,
                                Url = b.Url,
                                Reason = b.Reason,
                                Address = b.Address,
                                Name = b.Name,
                            }
                        ).FirstOrDefaultAsync();
            if (result != null)
            {
                var doctor = db.Doctor.Where(x => x.Id == result.CounselorId && x.Active == 1).FirstOrDefault();
                if (doctor != null)
                {
                    var accountDoctor = db.Accounts.Where(x => x.Id == doctor.AccountId).Select(x => x.Photo).FirstOrDefault();
                    if (accountDoctor != null)
                    {
                        result.Photo = accountDoctor;
                    }
                }
            };
            return result;
        }
        public async Task<Booking> Add(Booking obj)
        {
            if (db != null)
            {
                await db.Bookings.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(Booking obj)
        {
            if (db != null)
            {
                //Update that object
                db.Bookings.Attach(obj);
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;
                db.Entry(obj).Property(x => x.BookingTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.BookingStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.CounselorId).IsModified = true;
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Url).IsModified = true;
                db.Entry(obj).Property(x => x.Address).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.StartTime).IsModified = true;
                db.Entry(obj).Property(x => x.EndTime).IsModified = true;
                db.Entry(obj).Property(x => x.Reason).IsModified = true;
                db.Entry(obj).Property(x => x.Guide).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateByAccountant(Booking obj)
        {
            if (db != null)
            {
                //Update that object
                db.Bookings.Attach(obj);
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;
                db.Entry(obj).Property(x => x.BookingTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.BookingStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.CounselorId).IsModified = true;
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Url).IsModified = true;
                db.Entry(obj).Property(x => x.Address).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.StartTime).IsModified = true;
                db.Entry(obj).Property(x => x.EndTime).IsModified = true;
                db.Entry(obj).Property(x => x.Reason).IsModified = true;
                db.Entry(obj).Property(x => x.ServiceId).IsModified = true;
                db.Entry(obj).Property(x => x.Gender).IsModified = true;
                db.Entry(obj).Property(x => x.DoB).IsModified = true;
                db.Entry(obj).Property(x => x.Email).IsModified = true;
                db.Entry(obj).Property(x => x.Guide).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Booking obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Bookings.Attach(obj);
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
                var obj = await db.Bookings.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Bookings.Remove(obj);

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
                    from row in db.Bookings
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }

        public async Task<List<BookingViewModel>> ListTestByAccountId(string startTime, string endTime, int bookingStatusId, string filterId, int accountId)
        {
            return await (
                    from row in db.Bookings
                    from sv in db.Services
                    from bs in db.BookingStatuses
                    where
                    (row.ServiceId == sv.Id
                    && sv.ParentId == SystemConstant.SERVICE_TEST_PARENT
                    && row.AccountId == accountId
                    && bs.Id == row.BookingStatusId
                    && row.Active == 1 && sv.Active == 1 && bs.Active == 1
                    && (bookingStatusId == 0 ? row.Active == 1 : bs.Id == bookingStatusId)
                    && (filterId == null ? row.Active == 1 : row.Id.ToString().Contains(filterId.Trim().Replace("T","")))
                    && (startTime == null ? row.Active == 1 : row.StartTime >= ParseDate(startTime))
                    && (endTime == null ? row.Active == 1 : row.StartTime <= ParseDate(endTime).AddHours(23).AddMinutes(59).AddSeconds(59))
                    )
                    orderby row.Id descending
                    select new BookingViewModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        ServiceName = sv.Name,
                        BookingStatusName = bs.Name,
                        StartTime = row.StartTime,
                        Address = row.Address,
                        BookingStatusId = bs.Id,
                    }
                ).ToListAsync();
        }

        public static DateTime ParseDate(string dateString)
        {
            // Specify the exact format and use InvariantCulture to avoid culture-specific issues
            return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public async Task<DTResult<BookingViewModel>> ListServerSide(BookingDTParameters parameters)
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
            var query = from row in db.Bookings
                        join a in db.Accounts on row.AccountId equals a.Id
                        join ac in db.Doctor on row.CounselorId equals ac.Id
                        join bt in db.BookingTypes on row.BookingTypeId equals bt.Id
                        join bs in db.BookingStatuses on row.BookingStatusId equals bs.Id
                        join service in db.Services on row.ServiceId equals service.Id
                        where (row.Active == 1
                              && a.Active == 1
                              && bt.Active == 1
                              && bs.Active == 1
                              && ac.Active == 1
                              && service.Active == 1 && service.ParentId != SystemConstant.SERVICE_TEST_PARENT)
                        select new
                        {
                            row,
                            a,
                            ac,
                            bt,
                            bs,
                        };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.a.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.ac.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.bt.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.bs.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Url.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Address.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.StartTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                        case "url":
                            query = query.Where(c => (c.row.Url ?? "").Contains(fillter));
                            break;
                        case "address":
                            query = query.Where(c => (c.row.Address ?? "").Contains(fillter));
                            break;
                        case "info":
                            query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
                            break;
                        case "photo":
                            query = query.Where(c => (c.row.Photo ?? "").Contains(fillter));
                            break;
                        case "startTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.StartTime >= startDate && c.row.StartTime <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.StartTime.Date == date.Date);
                            }
                            break;
                        //case "endTime":
                        //    if (fillter.Contains(" - "))
                        //    {
                        //        var dates = fillter.Split(" - ");
                        //        var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //        var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                        //        query = query.Where(c => c.row.EndTime >= startDate && c.row.EndTime <= endDate);
                        //    }
                        //    else
                        //    {
                        //        var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //        query = query.Where(c => c.row.EndTime.Date == date.Date);
                        //    }
                        //    break;
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
                query = query.Where(c => parameters.AccountIds.Contains(c.row.AccountId));
            }

            if (parameters.CounselorIds.Count > 0)
            {
                query = query.Where(c => parameters.CounselorIds.Contains(c.ac.Id));
            }

            if (parameters.BookingTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.BookingTypeIds.Contains(c.row.BookingType.Id));
            }


            if (parameters.BookingStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.BookingStatusIds.Contains(c.row.BookingStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new BookingViewModel()
            {
                Id = c.row.Id,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                BookingTypeId = c.bt.Id,
                BookingTypeName = c.bt.Name,
                BookingStatusId = c.bs.Id,
                BookingStatusName = c.bs.Name,
                CounselorId = c.row.CounselorId,
                CounselorName = c.ac.Name,
                Active = c.row.Active,
                Name = c.row.Name,
                Url = c.row.Url,
                Address = c.row.Address,
                Info = c.row.Info,
                Photo = c.row.Photo,
                StartTime = c.row.StartTime,
                EndTime = c.row.EndTime,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<BookingViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            //DateTime now = DateTime.Now;
            //var sortDate = query2.OrderBy(x => x.StartTime).Select(x => x.StartTime >= now);

            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<BookingViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<DTResult<BookingViewModel>> ListTestServerSide(BookingDTParameters parameters)
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
            var query = from row in db.Bookings
                        join ac in db.Doctor on row.CounselorId equals ac.Id into doctorJoin
                        from ac in doctorJoin.DefaultIfEmpty()
                        join sv in db.Services on row.ServiceId equals sv.Id
                        join a in db.Accounts on row.AccountId equals a.Id
                        join bt in db.BookingTypes on row.BookingTypeId equals bt.Id
                        join bs in db.BookingStatuses on row.BookingStatusId equals bs.Id
                        where (row.Active == 1
                              && a.Active == 1
                              && bt.Active == 1
                              && bs.Active == 1
                              //&& ac.Active == 1
                              && sv.Active == 1
                              && sv.ParentId == SystemConstant.SERVICE_TEST_PARENT)
                        select new
                        {
                            row,
                            a,
                            ac,
                            bt,
                            bs,
                            sv
                        };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.a.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.ac.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.bt.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.bs.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Url.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Address.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.StartTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                        case "url":
                            query = query.Where(c => (c.row.Url ?? "").Contains(fillter));
                            break;
                        case "address":
                            query = query.Where(c => (c.row.Address ?? "").Contains(fillter));
                            break;
                        case "info":
                            query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
                            break;
                        case "photo":
                            query = query.Where(c => (c.row.Photo ?? "").Contains(fillter));
                            break;
                        case "startTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.StartTime >= startDate && c.row.StartTime <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.StartTime.Date == date.Date);
                            }
                            break;
                        //case "endTime":
                        //    if (fillter.Contains(" - "))
                        //    {
                        //        var dates = fillter.Split(" - ");
                        //        var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //        var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                        //        query = query.Where(c => c.row.EndTime >= startDate && c.row.EndTime <= endDate);
                        //    }
                        //    else
                        //    {
                        //        var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //        query = query.Where(c => c.row.EndTime.Date == date.Date);
                        //    }
                        //    break;
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
                query = query.Where(c => parameters.AccountIds.Contains(c.row.AccountId));
            }

            if (parameters.CounselorIds.Count > 0)
            {
                query = query.Where(c => parameters.CounselorIds.Contains(c.ac.Id));
            }

            if (parameters.BookingTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.BookingTypeIds.Contains(c.row.BookingType.Id));
            }


            if (parameters.BookingStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.BookingStatusIds.Contains(c.row.BookingStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new BookingViewModel()
            {
                Id = c.row.Id,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                BookingTypeId = c.bt.Id,
                BookingTypeName = c.bt.Name,
                BookingStatusId = c.bs.Id,
                BookingStatusName = c.bs.Name,
                CounselorId = c.row.CounselorId,
                CounselorName = c.ac == null ? "" : c.ac.Name,
                Active = c.row.Active,
                Name = c.row.Name,
                Url = c.row.Url,
                Address = c.row.Address,
                Info = c.row.Info,
                Photo = c.row.Photo,
                StartTime = c.row.StartTime,
                EndTime = c.row.EndTime,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<BookingViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            //DateTime now = DateTime.Now;
            //var sortDate = query2.OrderBy(x => x.StartTime).Select(x => x.StartTime >= now);

            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<BookingViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }


        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId and bookingStatusId</returns>
        public async Task<List<BookingViewModel>> ListBookingByBookingStatusId(int accountId, int bookingStatusId, int pageIndex, int pageSize)
        {
            if (db != null)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                var role = db.Accounts.Where(a => a.Id == accountId).Select(table => table.RoleId).FirstOrDefault();
                if (role == RoleId.DOCTOR)
                {
                    var result = await (
                                    from b in db.Bookings
                                    join a in db.Accounts on b.AccountId equals a.Id
                                    join bs in db.BookingStatuses on b.BookingStatusId equals bs.Id
                                    join bt in db.BookingTypes on b.BookingTypeId equals bt.Id
                                    join dt in db.Doctor on b.CounselorId equals dt.Id
                                    join sv in db.Services on dt.ServicesId equals sv.Id
                                    where a.Active == 1 && a.IsActivated == 1
                                    && b.Active == 1
                                    && bt.Active == 1
                                    && bs.Active == 1
                                    && dt.Active == 1
                                    && sv.Active == 1
                                    && b.CounselorId == accountId
                                    && b.BookingStatusId == bookingStatusId
                                    orderby b.StartTime descending

                                    select new BookingViewModel()
                                    {
                                        Id = b.Id,
                                        BookingTypeId = b.BookingTypeId,
                                        BookingTypeName = bt.Name,
                                        BookingStatusId = b.BookingStatusId,
                                        BookingStatusName = bs.Name,
                                        AccountId = b.AccountId,
                                        AccountName = db.Accounts.Where(a => a.Id == b.AccountId).Select(a => a.Name).FirstOrDefault(),
                                        CounselorId = (int)b.CounselorId,
                                        CounselorName = db.Doctor.Where(x => x.Id == b.CounselorId).Select(x => x.Name).FirstOrDefault(),
                                        Photo = db.Accounts.Where(a => a.Id == b.AccountId).Select(a => a.Photo).FirstOrDefault(),
                                        ServiceName = sv.Name,
                                        StartTime = b.StartTime,
                                        EndTime = b.EndTime,
                                        Info = b.Info,
                                        CreatedTime = b.CreatedTime
                                    }
                                ).Skip(offSet).Take(pageSize).ToListAsync();

                    return result;
                }
                else
                {
                    var result = await (
                                    from b in db.Bookings
                                    join a in db.Accounts on b.AccountId equals a.Id
                                    join bs in db.BookingStatuses on b.BookingStatusId equals bs.Id
                                    join bt in db.BookingTypes on b.BookingTypeId equals bt.Id
                                    join dt in db.Doctor on b.CounselorId equals dt.Id
                                    join sv in db.Services on dt.ServicesId equals sv.Id
                                    where a.Active == 1 && a.IsActivated == 1
                                    && b.Active == 1
                                    && bt.Active == 1
                                    && bs.Active == 1
                                    && dt.Active == 1
                                    && sv.Active == 1
                                    && b.AccountId == accountId
                                    && b.BookingStatusId == bookingStatusId
                                    orderby b.StartTime descending
                                    select new BookingViewModel()
                                    {
                                        Id = b.Id,
                                        BookingTypeId = b.BookingTypeId,
                                        BookingTypeName = bt.Name,
                                        BookingStatusId = b.BookingStatusId,
                                        BookingStatusName = bs.Name,
                                        AccountId = b.AccountId,
                                        AccountName = db.Accounts.Where(a => a.Id == b.AccountId).Select(a => a.Name).FirstOrDefault(),
                                        CounselorId = (int)b.CounselorId,
                                        CounselorName = db.Doctor.Where(x => x.Id == b.CounselorId).Select(x => x.Name).FirstOrDefault(),
                                        Photo = db.Accounts.Where(a => a.Id == dt.AccountId).Select(a => a.Photo).FirstOrDefault(),
                                        ServiceName = sv.Name,
                                        StartTime = b.StartTime,
                                        EndTime = b.EndTime,
                                        Info = b.Info,
                                        CreatedTime = b.CreatedTime
                                    }
                                ).Skip(offSet).Take(pageSize).ToListAsync();

                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId and bookingStatusId</returns>
        public async Task<List<BookingViewModel>> ListBookingByAccountId(int accountId)
        {
            if (db != null)
            {
                var role = db.Accounts.Where(a => a.Id == accountId).FirstOrDefault();

                if (role.RoleId == RoleId.DOCTOR)
                {
                    var doctor = db.Doctor.Where(x => x.AccountId == role.Id).FirstOrDefault();
                    return await (
                    from row in db.Bookings
                    join a in db.Accounts on row.AccountId equals a.Id
                    join bt in db.BookingTypes on row.BookingTypeId equals bt.Id
                    join bs in db.BookingStatuses on row.BookingStatusId equals bs.Id
                    join dt in db.Doctor on row.CounselorId equals dt.Id
                    join sv in db.Services on dt.ServicesId equals sv.Id
                    where row.Active == 1
                        && a.Active == 1
                        && bt.Active == 1
                        && bs.Active == 1
                        && (bs.Id != SystemConstant.BOOKING_STATUS_WAIT_ACCEPT)
                        && dt.Active == 1
                        && dt.Id == doctor.Id
                        && sv.Active == 1
                    select new BookingViewModel()
                    {
                        Id = row.Id,
                        AccountId = row.AccountId,
                        AccountName = db.Accounts.Where(ac => ac.Id == row.AccountId).Select(table => table.Name)
                              .FirstOrDefault(),
                        BookingTypeId = bt.Id,
                        BookingTypeName = bt.Name,
                        BookingStatusId = bs.Id,
                        BookingStatusName = bs.Name,
                        CounselorId = row.CounselorId,
                        CounselorName = db.Accounts.Where(ac => ac.Id == row.CounselorId).Select(table => table.Name)
                              .FirstOrDefault(),
                        Active = row.Active,
                        Name = row.Name,
                        Url = row.Url,
                        Address = row.Address,
                        Info = row.Info,
                        Photo = row.Photo,
                        StartTime = row.StartTime,
                        EndTime = row.EndTime,
                        CreatedTime = row.CreatedTime,
                    }).ToListAsync();
                }
                if (role.RoleId == RoleId.USER)
                {
                    return await (
                    from row in db.Bookings
                    join a in db.Accounts on row.AccountId equals a.Id
                    join bt in db.BookingTypes on row.BookingTypeId equals bt.Id
                    join bs in db.BookingStatuses on row.BookingStatusId equals bs.Id
                    where row.Active == 1
                        && a.Active == 1
                        && bt.Active == 1
                        && bs.Active == 1
                        && row.AccountId == accountId
                    select new BookingViewModel()
                    {
                        Id = row.Id,
                        AccountId = row.AccountId,
                        AccountName = db.Accounts.Where(ac => ac.Id == row.AccountId).Select(table => table.Name)
                              .FirstOrDefault(),
                        BookingTypeId = bt.Id,
                        BookingTypeName = bt.Name,
                        BookingStatusId = bs.Id,
                        BookingStatusName = bs.Name,
                        CounselorId = row.CounselorId,
                        CounselorName = db.Accounts.Where(ac => ac.Id == row.CounselorId).Select(table => table.Name)
                              .FirstOrDefault(),
                        Active = row.Active,
                        Name = row.Name,
                        Url = row.Url,
                        Address = row.Address,
                        Info = row.Info,
                        Photo = row.Photo,
                        StartTime = row.StartTime,
                        EndTime = row.EndTime,
                        CreatedTime = row.CreatedTime,
                    }).ToListAsync();
                }
            }

            return null;
        }
        public int CountListBookingByAccountId(int? accountId, int bookingStatusId)
        {
            int result = 0;
            if (db != null)
            {
                var role = db.Accounts.Where(a => a.Id == accountId).Select(table => table.RoleId).FirstOrDefault();
                if (role == RoleId.DOCTOR)
                {
                    result = (
                        from b in db.Bookings
                        join a in db.Accounts on b.CounselorId equals a.Id
                        join bs in db.BookingStatuses on b.BookingStatusId equals bs.Id
                        join bt in db.BookingTypes on b.BookingTypeId equals bt.Id
                        where a.Active == 1 && a.IsActivated == 1
                        && b.Active == 1
                        && bt.Active == 1
                        && bs.Active == 1
                        && (b.CounselorId == accountId || b.CounselorId == SystemConstant.ACCOUNT_DEFAULT_BOOKING)
                        && b.BookingStatusId == bookingStatusId
                        orderby b.StartTime descending

                        select b)
                    .Count();
                }
                else
                {
                    result = (
                    from row in db.Bookings
                    where (
                        row.Active == 1 && row.AccountId == accountId && row.BookingStatusId == bookingStatusId
                    )
                    select row)
                    .Count();
                }
            }
            return result;
        }
    }
}


