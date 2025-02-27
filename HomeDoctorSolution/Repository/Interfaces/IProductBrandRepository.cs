
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
        public interface IProductBrandRepository
        {
            Task <List< ProductBrand>> List();

            Task <List< ProductBrand>> Search(string keyword);

            Task <List< ProductBrand>> ListPaging(int pageIndex, int pageSize);

            Task <ProductBrand> Detail(int ? postId);

            Task <ProductBrand> Add(ProductBrand ProductBrand);

            Task Update(ProductBrand ProductBrand);

            Task Delete(ProductBrand ProductBrand);

            Task <int> DeletePermanently(int ? ProductBrandId);

            int Count();

            Task <DTResult<ProductBrand>> ListServerSide(ProductBrandDTParameters parameters);
        }
    }
