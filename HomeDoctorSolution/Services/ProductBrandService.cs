
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Repository;
        using HomeDoctorSolution.Services.Interfaces;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services
        {
            public class ProductBrandService : IProductBrandService
            {
                IProductBrandRepository productBrandRepository;
                public ProductBrandService(
                    IProductBrandRepository _productBrandRepository
                    )
                {
                    productBrandRepository = _productBrandRepository;
                }
                public async Task Add(ProductBrand obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await productBrandRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = productBrandRepository.Count();
                    return result;
                }
        
                public async Task Delete(ProductBrand obj)
                {
                    obj.Active = 0;
                    await productBrandRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await productBrandRepository.DeletePermanently(id);
                }
        
                public async Task<ProductBrand> Detail(int? id)
                {
                    return await productBrandRepository.Detail(id);
                }
        
                public async Task<List<ProductBrand>> List()
                {
                    return await productBrandRepository.List();
                }
        
                public async Task<List<ProductBrand>> ListPaging(int pageIndex, int pageSize)
                {
                    return await productBrandRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ProductBrand>> ListServerSide(ProductBrandDTParameters parameters)
                {
                    return await productBrandRepository.ListServerSide(parameters);
                }
        
                public async Task<List<ProductBrand>> Search(string keyword)
                {
                    return await productBrandRepository.Search(keyword);
                }
        
                public async Task Update(ProductBrand obj)
                {
                    await productBrandRepository.Update(obj);
                }
            }
        }
    
    