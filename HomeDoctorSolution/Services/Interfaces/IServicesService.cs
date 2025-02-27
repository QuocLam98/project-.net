
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IServicesService : IBaseService<Service>
    {
        Task<DTResult<ServicesViewModel>> ListServerSide(ServicesDTParameters parameters);
        Task<List<ServicesViewModel>> ListFourService();
        Task<List<ServicesViewModel>> Top4Service(int id);
        Task<List<Service>> ListPaging(int pageIndex, int pageSize, int clinicId);
        Task<List<ServicesViewModel>> ListPaging(int pageIndex, int pageSize, int clinicId,string keyword);
        Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize);
        Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize, int clinicId);
        Task<List<ServicesViewModel>> ListTest();
    }
}
