
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
        public interface IProductCategoryRepository
        {
            Task <List< ProductCategory>> List();

            Task <List< ProductCategory>> Search(string keyword);

            Task <List< ProductCategory>> ListPaging(int pageIndex, int pageSize);

            Task <ProductCategory> Detail(int ? postId);

            Task <ProductCategory> Add(ProductCategory ProductCategory);

            Task Update(ProductCategory ProductCategory);

            Task Delete(ProductCategory ProductCategory);

            Task <int> DeletePermanently(int ? ProductCategoryId);

            int Count();

            Task <DTResult<ProductCategory>> ListServerSide(ProductCategoryDTParameters parameters);
        }
    }
