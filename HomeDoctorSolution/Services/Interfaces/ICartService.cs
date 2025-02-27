
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        using HomeDoctor.Models.ModelDTO;

        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface ICartService : IBaseService<Cart>
            {
                Task<DTResult<CartViewModel>> ListServerSide(CartDTParameters parameters);
                Task<List<CartDetailViewModel>> GetProductsInCart(int? id);
                Task<Cart> CartInfo(int? id);
            }
        }
    