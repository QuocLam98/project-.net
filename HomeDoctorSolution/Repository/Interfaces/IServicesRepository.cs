
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
    public interface IServicesRepository
    {
        Task<List<Service>> List();
        Task<List<ServicesViewModel>> ListTest();
        Task<List<ServicesViewModel>> ListFourService();
        Task<List<Service>> Search(string keyword);
        Task<List<Service>> ListPaging(int pageIndex, int pageSize);
        Task<List<Service>> ListPaging(int pageIndex, int pageSize, int clinicId);
        Task<List<ServicesViewModel>> ListPaging(int pageIndex, int pageSize, int clinicId, string keyword);
        Task<Service> Detail(int? postId);

        Task<Service> Add(Service Services);

        Task Update(Service Services);

        Task Delete(Service Services);

        Task<int> DeletePermanently(int? ServicesId);

        Task<List<ServicesViewModel>> Top4Service(int id);

        int Count();

        Task<DTResult<ServicesViewModel>> ListServerSide(ServicesDTParameters parameters);
        Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize);
        Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize, int clinicId);
    }
}
