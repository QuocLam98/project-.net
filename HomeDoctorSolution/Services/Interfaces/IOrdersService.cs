
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Models.ViewModels.Product;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IOrdersService : IBaseService<Order>
    {
        Task<DTResult<OrdersViewModel>> ListServerSide(OrdersDTParameters parameters);
        Task<OrdersViewModel> DetailById(int? id);
        Task AddOrder(OrdersViewModel obj);
        Task UpdateOrder(OrdersViewModel obj);
        Task<List<OrdersViewModel>> ListById(int? id, int pageIndex, int pageSize);
        Task DeleteById(int? id, int accountId);
        Task<List<OrdersViewModel>> ListOrderByOrderStatusId(int accountId, int bookingStatusId, int pageIndex, int pageSize);
        Task<int> CountListOrderByAccountId(int? accountId, int bookingStatusId);
        Task<List<OrderCountViewModel>> CountListOrders(int accountId);
    }
}
