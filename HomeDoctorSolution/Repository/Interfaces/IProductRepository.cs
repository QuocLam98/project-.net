
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Util.Entities;
using HomeDoctor.Models.ModelDTO;
using HomeDoctor.Models.ViewModels.Product;

namespace HomeDoctorSolution.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> List();

        Task<List<Product>> Search(string keyword);

        Task<List<Product>> ListPaging(int pageIndex, int pageSize);
        Task<List<Product>> listBylistId(IEnumerable<int> listId);

        Task<Product> Detail(int? postId);

        Task<Product> Add(Product Product);

        Task Update(Product Product);

        Task Delete(Product Product);

        Task<int> DeletePermanently(int? ProductId);

        int Count();

        Task<DTResult<ProductViewModel>> ListServerSide(ProductDTParameters parameters);
        Task<PagingData<List<ProductViewModel>>> ListProductByName(ProductParameters parameter);
        Task<List<ProductViewModel>> ListProduct();
        Task<List<Product>> GetListProductByCategoryIdAsync(int categoryId);
        Task<bool> IsNameExist(int id, string name);
        Task<ProductViewModel> GetItemById(int id);
        Task<ProductViewModel> DetailProductVM(int id);
        Task<List<Product>> ListProductBrand(int Id);
        Task<List<ProductViewModel>> ListFourProductByTimeDesc();
        Task<PagingData<List<Product>>> SearchProductHomePageAsync(SearchingProductParameters parameter);
    }
}
