using HomeDoctorSolution.Repository.Interfaces;
using HomeDoctorSolution.Models;
using Microsoft.EntityFrameworkCore;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Globalization;
using HomeDoctorSolution.Models.ViewModel;
using HomeDoctorSolution.Constants;
using Microsoft.AspNetCore.Mvc;

namespace HomeDoctorSolution.Repository
{
    public class ConsultantRepository : IConsultantRepository
    {
        HomeDoctorContext db;

        public ConsultantRepository(HomeDoctorContext _db)
        {
            db = _db;
        }

        public async Task<Consultant> Detail(int? id)
        {
            if (db != null)
            {
                return await (
                    from row in db.Consultants
                    where row.BookingId == id
                    select row)
                    .FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Consultant>> ListByBookingId(int bookingId)
        {
            if (db != null)
            {
                return await (
                    from row in db.Consultants
                    where (row.BookingId == bookingId)
                    select row)
                    .ToListAsync();
            }
            return null;
        }

        public async Task<Consultant> DetailConsultantByBookingId(int id)
        {
            if (db != null)
            {
                return await (
                    from row in db.Consultants
                    where row.BookingId == id
                    select row)
                    .FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<Consultant> Add(Consultant obj)
        {
            if (db != null)
            {
                await db.Consultants.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }
        public async Task<ConsultantViewModal> DetailConsultant(int? id)
        {
            if (db != null)
            {
                var b1 = await (
                    from c in db.Consultants
                    join a in db.Accounts on c.AccountId equals a.Id
                    join b in db.Bookings on c.DoctorId equals b.CounselorId
                    where c.BookingId == id
                    orderby c.BookingId ascending
                    select new ConsultantViewModal
                    {
                        Id = c.Id,
                        Gender = a.Gender,
                        AccountName = a.Name,
                        Dob = a.Dob,
                        Address = a.Address,
                        AccountId = a.Id,
                        CounselorId = c.DoctorId.Value,
                        CounselorName = db.Accounts.Where(ac => ac.Id == c.DoctorId).Select(table => table.Name).FirstOrDefault(),
                        BookingId = b.Id,
                        BookingTypeId = b.BookingTypeId,
                        BookingTypeName = db.BookingTypes.Where(ac => ac.Id == b.BookingTypeId).Select(table => table.Name).FirstOrDefault(),
                        Description = c.Description,
                        Reason = c.Reason,
                        Symptom = c.Symptom,
                        Religiorelationship = c.Religiorelationship,
                        History = c.History,
                        AssessmentResult = c.AssessmentResult,
                        Target = c.Target,
                        ConsultingTime = c.ConsultingTime,
                        ConsultingResults = c.ConsultingResults,
                        ConsultingPlan = c.ConsultingPlan,
                        ReligiousNation = c.ReligiousNation,
                        CulturalLevel = c.CulturalLevel,
                        Implementation = c.Implementation,
                        Rating = c.Rating,
                        Review = c.Review,
                        Form = c.Form,
                        CreatedTime = (DateTime)c.CreatedTime,
                    }).FirstOrDefaultAsync();
                return b1;
            }
            return null;
        }

        public async Task Update(Consultant obj)
        {
            if (db != null)
            {
                //Update that object
                db.Consultants.Attach(obj);
                db.Entry(obj).Property(x => x.Reason).IsModified = true;
                db.Entry(obj).Property(x => x.Symptom).IsModified = true;
                db.Entry(obj).Property(x => x.Religiorelationship).IsModified = true;
                db.Entry(obj).Property(x => x.History).IsModified = true;
                db.Entry(obj).Property(x => x.AssessmentResult).IsModified = true;
                db.Entry(obj).Property(x => x.Target).IsModified = true;
                db.Entry(obj).Property(x => x.ConsultingResults).IsModified = true;
                db.Entry(obj).Property(x => x.ConsultingPlan).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.ReligiousNation).IsModified = true;
                db.Entry(obj).Property(x => x.CulturalLevel).IsModified = true;
                db.Entry(obj).Property(x => x.Implementation).IsModified = true;
                db.Entry(obj).Property(x => x.ConsultingTime).IsModified = true;
                db.Entry(obj).Property(x => x.Form).IsModified = true;
                db.Entry(obj).Property(x => x.Rating).IsModified = true;
                db.Entry(obj).Property(x => x.Review).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        /// Author: HuyDQ
        /// Created: 08/11/2023
        /// Description: check tài khoản nào có thể xem được view màn phiếu đánh giá
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckAccountViewConsultant(int bookingId, int accountId)
        {
            var booking = await (
                    from row in db.Consultants
                    where row.BookingId == bookingId
                    select row)
                    .FirstOrDefaultAsync();
            if (booking != null)
            {
                if (booking.AccountId == accountId || booking.DoctorId == accountId)
                {
                    return true;
                }
                if (booking.AccountId != accountId && booking.DoctorId != accountId)
                {
                    return false;
                }
            }

            return false;
        }
        public Task<List<Consultant>> GetRatingOfCounselor()
        {
            throw new NotImplementedException();
        }

        public Task<Consultant> DetailByBookingId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ConsulantViewModel>> ListReviewConsultant(int counselorsId, int pageIndex, int pageSize)
        {
            if (db != null)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                var total = await (from c in db.Consultants
                                   join a in db.Accounts on c.AccountId equals a.Id
                                   where c.DoctorId == counselorsId
                                   select new ConsulantViewModel
                                   {
                                       CounselorsId = c.DoctorId.Value,
                                   }).CountAsync();
                var hasNext = (int)Math.Ceiling(total / (double)pageSize) > pageIndex;
                return await (
                                    from c in db.Consultants
                                    join a in db.Accounts on c.AccountId equals a.Id
                                    where c.DoctorId == counselorsId
                                    select new ConsulantViewModel
                                    {
                                        CounselorsId = c.DoctorId.Value,
                                        CounselorsName = a.Name,
                                        Photo = a.Photo,
                                        Rating = c.Rating,
                                        Review = c.Review,
                                        CreatedTime = (DateTime)c.CreatedTime,
                                        HasNext = hasNext,
                                        Total = total
                                    }
                                ).Skip(offSet)
                                .Take(pageSize)
                                .ToListAsync();
            }
            return null;
        }

        public async Task<List<Consultant>> ListConsultantByAccountId(int accountId)
        {
            return await (from c in db.Consultants
                          where c.AccountId == accountId
                          select c
                          ).OrderByDescending(c => c.CreatedTime).ToListAsync();
        }

        public async Task<int> DeletePermanently(int? objId)
        {
            int result = 0;
            if (db != null)
            {
                //Find the obj for specific obj id
                var obj = await db.Consultants.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Consultants.Remove(obj);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
    }
}
