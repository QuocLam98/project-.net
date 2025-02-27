
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeDoctorSolution.Util.Entities;
using HomeDoctor.Models.ModelDTO;
using HomeDoctor.Models.ViewModels.Product;

namespace HomeDoctorSolution.Services
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;
        public ProductService(
            IProductRepository _productRepository
            )
        {
            productRepository = _productRepository;
        }
        public async Task Add(Product obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await productRepository.Add(obj);
        }

        public async Task<HomeDoctorResponse> AddProduct(Product obj)
        {
            var checkExistName = await IsNameExist(0, obj.Name);
            if (checkExistName)
            {
                return HomeDoctorResponse.BadRequest("Tên sản phẩm đã tồn tại");
            }
            else
            {
                obj.Active = 1;
                obj.CreatedTime = DateTime.Now;
                var data = await productRepository.Add(obj);
                return HomeDoctorResponse.Success(data);
            }
        }

        public int Count()
        {
            var result = productRepository.Count();
            return result;
        }

        public async Task Delete(Product obj)
        {
            obj.Active = 0;
            await productRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await productRepository.DeletePermanently(id);
        }

        public async Task<Product> Detail(int? id)
        {
            return await productRepository.Detail(id);
        }

        public Task<ProductViewModel> GetItemById(int id)
        {
            return productRepository.GetItemById(id);
        }

        public async Task<List<Product>> List()
        {
            return await productRepository.List();
        }
        public async Task<List<Product>> listBylistId(IEnumerable<int> listId)
        {
            return await productRepository.listBylistId(listId);
        }
        public async Task<List<Product>> ListPaging(int pageIndex, int pageSize)
        {
            return await productRepository.ListPaging(pageIndex, pageSize);
        }

        public Task<PagingData<List<ProductViewModel>>> ListProductByName(ProductParameters parameter)
        {
           return productRepository.ListProductByName(parameter);
        }

        /// <summary>
        /// Author : HoanNK 
        /// CreatedTime: 18/12
        /// Description: List Product
        /// </summary>
        /// <returns></returns>
        public Task<List<ProductViewModel>> ListProduct()
        {
            return productRepository.ListProduct();
        }

        public async Task<DTResult<ProductViewModel>> ListServerSide(ProductDTParameters parameters)
        {
            return await productRepository.ListServerSide(parameters);
        }

        public async Task<List<Product>> Search(string keyword)
        {
            return await productRepository.Search(keyword);
        }

        public async Task Update(Product obj)
        {
            await productRepository.Update(obj);
        }

        public async Task<List<Product>> GetListProductByCategoryIdAsync(int categoryId)
        {
            return await productRepository.GetListProductByCategoryIdAsync(categoryId);
        }

        public async Task<bool> IsNameExist(int id, string name)
        {
            return await productRepository.IsNameExist(id, name);
        }

        public async Task<ProductViewModel> DetailProductVM(int id)
        {
            return await productRepository.DetailProductVM(id); 
        }
        public async Task<List<Product>> ListProductBrand(int id)
        {
            return await productRepository.ListProductBrand(id);
        }
        public async Task<List<ProductViewModel>> ListFourProductByTimeDesc()
        {
            return await productRepository.ListFourProductByTimeDesc();
        }

        public async Task<PagingData<List<Product>>> SearchProductHomePageAsync(SearchingProductParameters parameter)
        {
            return await productRepository.SearchProductHomePageAsync(parameter);
        }
    }
}

