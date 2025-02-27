
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctor.Models.ModelDTO;
using HomeDoctorSolution.Util.Entities;
using HomeDoctor.Models.ViewModels.Product;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<DTResult<ProductViewModel>> ListServerSide(ProductDTParameters parameters);
        Task<List<ProductViewModel>> ListProduct();
        Task<ProductViewModel> GetItemById(int id);
        Task<PagingData<List<ProductViewModel>>> ListProductByName(ProductParameters parameter);
        Task<List<Product>> GetListProductByCategoryIdAsync(int categoryId);
        Task<bool> IsNameExist(int id, string name);
        Task<HomeDoctorResponse> AddProduct(Product obj);
        Task<ProductViewModel> DetailProductVM(int id);
        Task<List<Product>> ListProductBrand(int id);
        Task<List<ProductViewModel>> ListFourProductByTimeDesc();
        Task<PagingData<List<Product>>> SearchProductHomePageAsync(SearchingProductParameters parameter);
        Task<List<Product>> listBylistId(IEnumerable<int> listId);
    }
}
