using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IProductStatusService : IBaseService<ProductStatus>
    {
        Task<DTResult<ProductStatus>> ListServerSide(ProductStatusDTParameters parameters);
        Task<bool> IsNameExist(int id, string name);
    }
}
