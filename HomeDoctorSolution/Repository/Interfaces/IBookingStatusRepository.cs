
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
        public interface IBookingStatusRepository
        {
            Task <List< BookingStatus>> List();

            Task <List< BookingStatus>> Search(string keyword);

            Task <List< BookingStatus>> ListPaging(int pageIndex, int pageSize);

            Task <BookingStatus> Detail(int ? postId);

            Task <BookingStatus> Add(BookingStatus BookingStatus);

            Task Update(BookingStatus BookingStatus);

            Task Delete(BookingStatus BookingStatus);

            Task <int> DeletePermanently(int ? BookingStatusId);

            int Count();

            Task <DTResult<BookingStatus>> ListServerSide(BookingStatusDTParameters parameters);
        }
    }
