
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IOrderDetailService : IBaseService<OrderDetail>
    {
        Task<DTResult<OrderDetailViewModel>> ListServerSide(OrderDetailDTParameters parameters);
        Task<List<OrderDetail>> AddMany(List<OrderDetail> orderDetails);
    }
}
