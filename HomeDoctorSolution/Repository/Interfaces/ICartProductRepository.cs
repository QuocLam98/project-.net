
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HomeDoctorSolution.Repository
{
    public interface ICartProductRepository
    {
        Task<List<CartProduct>> List();

        Task<List<CartProduct>> Search(string keyword);

        Task<List<CartProduct>> ListPaging(int pageIndex, int pageSize);

        Task<CartProduct> Detail(int? postId);

        Task<CartProduct> Add(CartProduct CartProduct);
        Task<CartProduct> UpdateQuantity(CartProduct CartProduct);

        Task Update(CartProduct CartProduct);

        Task Delete(CartProduct CartProduct);

        Task<int> DeletePermanently(int? CartProductId);

        int Count();

        Task<Cart> FindCartByAccountIdAsync(int? accountId);
        Task<bool> FindCartProductById(int? productId, int? cartId);
        Task<DTResult<CartProductViewModel>> ListServerSide(CartProductDTParameters parameters);
        DatabaseFacade GetDatabase();
    }
}
