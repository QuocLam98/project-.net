
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IDoctorsService : IBaseService<Doctor>
    {
        Task<DTResult<DoctorsViewModel>> ListServerSide(DoctorsDTParameters parameters);
        Task<List<Doctors>> ListDoctor();
        Task<List<DoctorsViewModel>> ListThreeDoctorByTimeDesc();
        Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize);
        Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId, int healthFacilityId);
        //Filter cho trang danh sach bac si
        Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId, int healthFacilityId,string keyword, string district);
        Task<int> listPagingViewModelCount( int serviceId, int healthFacilityId,string keyword, string district);
        Task<Doctor> DetailViewModel(int? id);
        Task<List<Doctor>> get6DoctorOutstanding();
        Task<List<DoctorsViewModel>> ListDoctorBooking(int AccountId);
    }
}
