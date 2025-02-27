
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Models.ViewModels.Product;

namespace HomeDoctorSolution.Services
{
    public class OrdersService : IOrdersService
    {
        IOrdersRepository ordersRepository;
        IOrderDetailRepository orderDetailRepository;
        IOrderDetailService orderDetailService;
        IProductService productService;
        IPromotionService promotionService;
        private ICartService cartService;
        private readonly IMapper _mapper;
        public OrdersService(
            IOrdersRepository _ordersRepository,
            IOrderDetailRepository _orderDetailRepository,
            IOrderDetailService _orderDetailService,
            IProductService _productService,
            IPromotionService _promotionService,
            IMapper mapper, 
            ICartService cartService
            )
        {
            orderDetailRepository = _orderDetailRepository;
            ordersRepository = _ordersRepository;
            orderDetailService = _orderDetailService;
            productService = _productService;
            promotionService = _promotionService;
            _mapper = mapper;
            this.cartService = cartService;
        }
        public async Task Add(Order obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await ordersRepository.Add(obj);
        }

        public int Count()
        {
            var result = ordersRepository.Count();
            return result;
        }

        public async Task Delete(Order obj)
        {
            obj.Active = 0;
            await ordersRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await ordersRepository.DeletePermanently(id);
        }

        public async Task<Order> Detail(int? id)
        {
            return await ordersRepository.Detail(id);
        }

        public async Task<List<Order>> List()
        {
            return await ordersRepository.List();
        }

        public async Task<List<Order>> ListPaging(int pageIndex, int pageSize)
        {
            return await ordersRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<OrdersViewModel>> ListServerSide(OrdersDTParameters parameters)
        {
            return await ordersRepository.ListServerSide(parameters);
        }

        public async Task<List<Order>> Search(string keyword)
        {
            return await ordersRepository.Search(keyword);
        }

        public async Task Update(Order obj)
        {
            await ordersRepository.Update(obj);
        }

        //Tạo mới đơn hàng full
        public async Task AddOrder(OrdersViewModel obj)
        {
            //0. Khai báo constant
            using (var database = ordersRepository.DataBase().BeginTransaction())
            {
                //Cấu hình
                List<OrderDetail> ordersDetails = _mapper.Map<List<OrderDetail>>(obj.OrderDetails);
                for (int i = 0; i < ordersDetails.Count; i++)
                {
                    var item = ordersDetails[i];
                    var product = await productService.Detail(item.ProductId);
                    item.Price = product.Price;
                    item.Active = 1;
                    item.Quantity = obj.OrderDetails[i].Quantity;
                    item.FinalPrice = product.Price * obj.OrderDetails[i].Quantity;
                    item.OrderDetailStatusId = SystemConstant.ORDER_DETAIL_STATUS_DEFAULT;
                    item.CreatedTime = DateTime.Now;
                }
                var order = _mapper.Map<Order>(obj);
                var discount = order.PromotionId != null ? promotionService.Detail(obj.PromotionId).Result.Discount : 0;
                order.Active = 1;
                order.TotalPrice = ordersDetails.Sum(x => x.FinalPrice);
                order.FinalPrice = (order.TotalPrice + (order.TotalShipFee == null ? 0 : order.TotalShipFee)) * (discount == 0 ? 1 : (discount / 100));
                order.OrderTypeId = 10000002;
                order.OrderStatusId = 1000001;
                order.OrderStatusShipId = 1000001;
                order.OrderPaymentStatusId = 1000001;
                order.AccountId = obj.AccountId;
                order.ShipAddressDetail = obj.ShipAddressDetail;
                order.ShipRecipientPhone = obj.AccountPhone;
                order.ShipRecipientName = obj.AccountName;
                order.Name = "ĐH";
                order.CreatedTime = DateTime.Now;
                order.ShipProvinceAddress = obj.ShipProvinceAddress;
                order.ShipDistrictAddress = obj.ShipDistrictAddress;
                order.ShipWardAddress = obj.ShipWardAddress;
                order.Description = obj.Description;
                order.OrderDetails = null;
                //Add
                var newOrder = await ordersRepository.Add(order);
                if (newOrder != null)
                {
                    ordersDetails.ForEach(item => item.OrderId = order.Id);
                    var newOrderDetails = await orderDetailService.AddMany(ordersDetails);
                    Cart cart = await cartService.CartInfo(obj.AccountId);
                    if (cart != null)
                    {
                        await cartService.DeletePermanently(cart.Id);
                    }
                    if (!(newOrderDetails.Count > 0))
                    {
                        await database.RollbackAsync();
                    }
                    obj.Id = newOrder.Id;
                }
                else
                {
                    await database.RollbackAsync();
                }
                await database.CommitAsync();
            }
        }

        public async Task UpdateOrder(OrdersViewModel obj)
        {
            using (var database = ordersRepository.DataBase().BeginTransaction())
            {
                try
                {
                    //Map order detail
                    List<OrderDetail> ordersDetails = _mapper.Map<List<OrderDetail>>(obj.OrderDetails);

                    //Map order
                    var order = _mapper.Map<Order>(obj);

                    //Logic
                    for (int i = 0; i < ordersDetails.Count; i++)
                    {
                        var item = ordersDetails[i];
                        var product = await productService.Detail(item.ProductId);
                        item.OrderId = order.Id;
                        item.Price = product.Price;
                        item.Active = 1;
                        item.Quantity = obj.OrderDetails[i].Quantity;
                        item.FinalPrice = product.Price * obj.OrderDetails[i].Quantity;
                        item.OrderDetailStatusId = SystemConstant.ORDER_DETAIL_STATUS_DEFAULT;
                        item.CreatedTime = DateTime.Now;
                    }
                    //Check delete order detail
                    var listOldOderDetail = await orderDetailRepository.ListByOrderId(order.Id);
                    await orderDetailRepository.CompareAndUpdate(ordersDetails, listOldOderDetail);

                    var discount = order.PromotionId != null ? promotionService.Detail(obj.PromotionId).Result.Discount : 0;
                    order.TotalPrice = ordersDetails.Sum(x => x.FinalPrice);
                    order.FinalPrice = (order.TotalPrice + (order.TotalShipFee == null ? 0 : order.TotalShipFee)) * (discount == 0 ? 1 : (discount / 100));
                    await orderDetailRepository.UpdateMany(ordersDetails);
                    await ordersRepository.Update(order);
                    await database.CommitAsync();
                }
                catch (Exception e)
                {
                    await database.RollbackAsync();
                }
            }
        }

        public async Task<OrdersViewModel> DetailById(int? id)
        {
            return await ordersRepository.DetailById(id);
        }

        public async Task<List<OrdersViewModel>> ListById(int? id, int pageIndex, int pageSize)
        {
            return await ordersRepository.ListById(id, pageIndex, pageSize);
        }

        public async Task DeleteById(int? id, int accountId)
        {
            await ordersRepository.DeleteById(id, accountId);
        }

        public async Task<List<OrdersViewModel>> ListOrderByOrderStatusId(int accountId, int orderStatusId, int pageIndex, int pageSize)
        {
            return await ordersRepository.ListOrderByOrderStatusId(accountId, orderStatusId, pageIndex, pageSize);
        }

        public async Task<int> CountListOrderByAccountId(int? accountId, int orderstatusId)
        {
            return await ordersRepository.CountListOrderByAccountId(accountId, orderstatusId);
        }

        public async Task<List<OrderCountViewModel>> CountListOrders(int accountId)
        {
            return await ordersRepository.CountListOrders(accountId);
        }
    }
}

