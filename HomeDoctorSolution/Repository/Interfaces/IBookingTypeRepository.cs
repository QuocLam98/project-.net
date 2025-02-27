
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
        public interface IBookingTypeRepository
        {
            Task <List< BookingType>> List();

            Task <List< BookingType>> Search(string keyword);

            Task <List< BookingType>> ListPaging(int pageIndex, int pageSize);

            Task <BookingType> Detail(int ? postId);

            Task <BookingType> Add(BookingType BookingType);

            Task Update(BookingType BookingType);

            Task Delete(BookingType BookingType);

            Task <int> DeletePermanently(int ? BookingTypeId);

            int Count();

            Task <DTResult<BookingType>> ListServerSide(BookingTypeDTParameters parameters);
        }
    }
