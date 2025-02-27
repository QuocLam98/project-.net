
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface IBookingTypeService : IBaseService<BookingType>
            {
                Task<DTResult<BookingType>> ListServerSide(BookingTypeDTParameters parameters);
            }
        }
    