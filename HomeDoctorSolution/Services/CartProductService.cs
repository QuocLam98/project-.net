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
    public class CartProductService : ICartProductService
    {
        ICartProductRepository cartProductRepository;
        ICartRepository cartRepository;

        public CartProductService(
            ICartProductRepository _cartProductRepository,
            ICartRepository _cartRepository
        )
        {
            cartProductRepository = _cartProductRepository;
            cartRepository = _cartRepository;
        }

        public async Task Add(CartProduct obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await cartProductRepository.Add(obj);
        }

        public async Task<bool> AddProductCart(CartProductViewModel obj)
        {
            var isCartExists = await cartProductRepository.FindCartByAccountIdAsync(obj.AccountId);

            if (isCartExists != null)
            {
                var isProductExist = await cartProductRepository.FindCartProductById(obj.ProductId, isCartExists.Id);

                var productCart = new CartProduct
                {
                    Active = 1,
                    Name = "Giỏ hàng sản phẩm",
                    CreatedTime = DateTime.Now,
                    ProductId = obj.ProductId,
                    CartId = isCartExists.Id,
                    Quantity = obj.Quantity
                };

                if (isProductExist)
                {
                    await cartProductRepository.UpdateQuantity(productCart);
                }
                else
                {
                    await cartProductRepository.Add(productCart);
                }

                return true;
            }

            using (var database = cartProductRepository.GetDatabase().BeginTransaction())
            {
                try
                {
                    var cart = new Cart
                    {
                        Active = 1,
                        Name = "Giỏ hàng",
                        CreatedTime = DateTime.Now,
                        AccountId = obj.AccountId
                    };

                    var cartId = await cartRepository.Add(cart);

                    if (cartId.Id > 0)
                    {
                        var productCart = new CartProduct
                        {
                            Active = 1,
                            Name = "Giỏ hàng sản phẩm",
                            CreatedTime = DateTime.Now,
                            ProductId = obj.ProductId,
                            CartId = cartId.Id,
                            Quantity = obj.Quantity
                        };

                        await cartProductRepository.Add(productCart);
                    }

                    await database.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await database.RollbackAsync();
                    return false;
                }
            }
        }


        // public async Task<bool> UpdateQuantity(CartProductViewModel obj)
        // {
        //     var isCartExists = await cartProductRepository.FindCartByAccountIdAsync(obj.AccountId);
        //
        //     var productCart = new CartProduct
        //     {
        //         CreatedTime = DateTime.Now,
        //         ProductId = obj.ProductId,
        //         CartId = isCartExists.Id,
        //         Quantity = obj.Quantity
        //     };
        //     if (isCartExists != null)
        //     {
        //         //add cart product
        //         await cartProductRepository.UpdateQuantity(productCart);
        //
        //         return true;
        //     }
        //     return false;
        // }

        public int Count()
        {
            var result = cartProductRepository.Count();
            return result;
        }

        public async Task Delete(CartProduct obj)
        {
            obj.Active = 0;
            await cartProductRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await cartProductRepository.DeletePermanently(id);
        }

        public async Task<CartProduct> Detail(int? id)
        {
            return await cartProductRepository.Detail(id);
        }

        public async Task<List<CartProduct>> List()
        {
            return await cartProductRepository.List();
        }

        public async Task<List<CartProduct>> ListPaging(int pageIndex, int pageSize)
        {
            return await cartProductRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<CartProductViewModel>> ListServerSide(CartProductDTParameters parameters)
        {
            return await cartProductRepository.ListServerSide(parameters);
        }

        public async Task<List<CartProduct>> Search(string keyword)
        {
            return await cartProductRepository.Search(keyword);
        }

        public async Task Update(CartProduct obj)
        {
            await cartProductRepository.Update(obj);
        }
    }
}