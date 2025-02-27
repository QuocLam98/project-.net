
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
    public interface IDoctorsRepository
    {
        Task<List<Doctor>> List();

        Task<List<Doctor>> Search(string keyword);

        Task<List<Doctor>> ListPaging(int pageIndex, int pageSize);

        Task<Doctor> Detail(int? postId);

        Task<Doctor> Add(Doctor Doctor);

        Task Update(Doctor Doctor);

        Task Delete(Doctor Doctor);

        Task<int> DeletePermanently(int? DoctorsId);
        Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize);
        Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId, int healthFacilityId);
        Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId, int healthFacilityId,string keyword, string district);
        Task<int> listPagingViewModelCount(int serviceId, int healthFacilityId,string keyword, string district);

        int Count();
        Task<Doctor> DetailViewModel(int? id);
        Task<DTResult<DoctorsViewModel>> ListServerSide(DoctorsDTParameters parameters);
        Task<List<Doctors>> ListDoctor();
        Task<List<DoctorsViewModel>> ListThreeDoctorByTimeDesc();
        Task<List<Doctor>> get6DoctorOutstanding();
        Task<List<DoctorsViewModel>> ListDoctorBooking(int AccountId);
    }
}
