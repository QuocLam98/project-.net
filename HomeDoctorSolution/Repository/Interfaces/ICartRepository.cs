
    using HomeDoctorSolution.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HomeDoctor.Models.ModelDTO;
    using HomeDoctorSolution.Util;
    using HomeDoctorSolution.Util.Parameters;
    using HomeDoctorSolution.Models.ViewModels;


    namespace HomeDoctorSolution.Repository
    {
        public interface ICartRepository
        {
            Task <List< Cart>> List();

            Task <List< Cart>> Search(string keyword);

            Task <List< Cart>> ListPaging(int pageIndex, int pageSize);

            Task <Cart> Detail(int ? postId);

            Task <Cart> Add(Cart Cart);

            Task Update(Cart Cart);

            Task Delete(Cart Cart);

            Task <int> DeletePermanently(int ? CartId);

            int Count();

            Task <DTResult<CartViewModel>> ListServerSide(CartDTParameters parameters);
            Task<List<CartDetailViewModel>> GetProductsInCart(int? id);
            Task<Cart> CartInfo(int? id);
        }
    }
