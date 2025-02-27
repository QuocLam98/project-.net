
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Repository;
        using HomeDoctorSolution.Services.Interfaces;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using HomeDoctor.Models.ModelDTO;

        namespace HomeDoctorSolution.Services
        {
            public class CartService : ICartService
            {
                ICartRepository cartRepository;
                public CartService(
                    ICartRepository _cartRepository
                    )
                {
                    cartRepository = _cartRepository;
                }
                public async Task Add(Cart obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await cartRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = cartRepository.Count();
                    return result;
                }
        
                public async Task Delete(Cart obj)
                {
                    obj.Active = 0;
                    await cartRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await cartRepository.DeletePermanently(id);
                }
        
                public async Task<Cart> Detail(int? id)
                {
                    return await cartRepository.Detail(id);
                }
        
                public async Task<List<Cart>> List()
                {
                    return await cartRepository.List();
                }
        
                public async Task<List<Cart>> ListPaging(int pageIndex, int pageSize)
                {
                    return await cartRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<CartViewModel>> ListServerSide(CartDTParameters parameters)
                {
                    return await cartRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Cart>> Search(string keyword)
                {
                    return await cartRepository.Search(keyword);
                }
        
                public async Task Update(Cart obj)
                {
                    await cartRepository.Update(obj);
                }
                public async Task<List<CartDetailViewModel>> GetProductsInCart(int? id)
                {
                    return await cartRepository.GetProductsInCart(id);
                }

                public async Task<Cart> CartInfo(int? id)
                {
                    return await cartRepository.CartInfo(id);
                }
            }
        }
    
    