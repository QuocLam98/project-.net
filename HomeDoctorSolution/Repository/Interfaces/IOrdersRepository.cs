
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Models.ViewModels.Product;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HomeDoctorSolution.Repository
{
    public interface IOrdersRepository
    {
        Task<List<Order>> List();

        Task<List<Order>> Search(string keyword);

        Task<List<Order>> ListPaging(int pageIndex, int pageSize);

        Task<Order> Detail(int? postId);

        Task<Order> Add(Order Order);

        Task Update(Order Order);

        Task Delete(Order Order);
        Task DeleteById(int? id, int accountId);

        Task<int> DeletePermanently(int? OrdersId);

        int Count();

        Task<DTResult<OrdersViewModel>> ListServerSide(OrdersDTParameters parameters);

        Task<OrdersViewModel> DetailById(int? id);
        Task<List<OrdersViewModel>> ListById(int? id, int pageIndex, int pageSize);
        Task<List<OrdersViewModel>> ListOrderByOrderStatusId(int accountId, int bookingStatusId, int pageIndex, int pageSize);

        Task<int> CountListOrderByAccountId(int? accountId, int bookingStatusId);
        Task<List<OrderCountViewModel>> CountListOrders(int accountId);
        // Task<OrderViewModel> DetailByAccountId(int? id);
        DatabaseFacade DataBase();
    }
}
