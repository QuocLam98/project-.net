
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctor.Models.ViewModels;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IBookingService : IBaseService<Booking>
    {
        Task<DTResult<BookingViewModel>> ListServerSide(BookingDTParameters parameters);

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
        /// <returns>Update booking by roleId</returns>
        Task UpdateByRole(Booking obj, int? roleId);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId</returns>
        Task<List<BookingViewModel>> ListBookingByAccountId(int accountId);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>add booking byle Role</returns>
        Task AddByRole(Booking obj, int roleId);
        int CountListBookingByAccountId(int? accountId, int bookingStatusId);
        Task<BookingViewModel> DetailViewModel(int id);
        Task<BookingStaticsCount> CountStatics();
        Task<BookingStaticsCount> CountStaticsTest();
        Task<List<BookingViewModel>> ListTestByAccountId(string startTime, string endTime, int bookingStatusId, string filterId, int accountId);
        Task<DTResult<BookingViewModel>> ListTestServerSide(BookingDTParameters parameters);
    }
}
