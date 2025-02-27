
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface ICartProductService : IBaseService<CartProduct>
    {
        Task<DTResult<CartProductViewModel>> ListServerSide(CartProductDTParameters parameters);
        Task<bool> AddProductCart(CartProductViewModel obj);
        // Task<bool> UpdateQuantity(CartProductViewModel obj);
    }
}
