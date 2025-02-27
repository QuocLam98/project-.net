
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
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> List();

        Task<List<OrderDetail>> Search(string keyword);

        Task<List<OrderDetail>> ListPaging(int pageIndex, int pageSize);

        Task<OrderDetail> Detail(int? postId);

        Task<OrderDetail> Add(OrderDetail OrderDetail);

        Task Update(OrderDetail OrderDetail);

        Task Delete(OrderDetail OrderDetail);

        Task<int> DeletePermanently(int? OrderDetailId);

        int Count();

        Task<DTResult<OrderDetailViewModel>> ListServerSide(OrderDetailDTParameters parameters);

        Task<List<OrderDetail>> AddMany(List<OrderDetail> orderDetails);

        Task<List<OrderDetail>> UpdateMany(List<OrderDetail> orderDetails);

        Task<List<OrderDetail>> CompareAndUpdate(List<OrderDetail> listNew, List<OrderDetail> listOld);

        Task<List<OrderDetail>> ListByOrderId(int orderId);
    }
}
