
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;


namespace HomeDoctorSolution.Repository
{
    public interface IBookingRepository
    {
        Task<List<Booking>> List();

        Task<List<Booking>> ListTest();

        Task<List<Booking>> Search(string keyword);

        Task<List<Booking>> ListPaging(int pageIndex, int pageSize);

        Task<Booking> Detail(int? postId);

        Task<Booking> Add(Booking Booking);

        Task Update(Booking Booking);
        Task UpdateByAccountant(Booking obj);

        Task Delete(Booking Booking);

        Task<int> DeletePermanently(int? BookingId);

        int Count();

        Task<DTResult<BookingViewModel>> ListServerSide(BookingDTParameters parameters);
        Task<DTResult<BookingViewModel>> ListTestServerSide(BookingDTParameters parameters);
        Task<List<BookingViewModel>> ListTestByAccountId(string startTime, string endTime, int bookingStatusId, string filterId, int accountId);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId and bookingStatusId</returns>
        Task<List<BookingViewModel>> ListBookingByBookingStatusId(int accountId, int bookingStatusId, int pageIndex, int pageSize);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId</returns>
        Task<List<BookingViewModel>> ListBookingByAccountId(int accountId);
        int CountListBookingByAccountId(int? accountId, int bookingStatusId);
        Task<BookingViewModel> DetailViewModel(int id);
    }
}
