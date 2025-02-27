using HomeDoctorSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Globalization;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HomeDoctorSolution.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        HomeDoctorContext db;

        public OrdersRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Order>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Orders
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Order>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Orders
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Order>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Orders
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }


        public async Task<Order> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Orders.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }

            return null;
        }


        public async Task<Order> Add(Order obj)
        {
            if (db != null)
            {
                await db.Orders.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }

            return null;
        }


        public async Task Update(Order obj)
        {
            if (db != null)
            {
                //Update that object
                db.Orders.Attach(obj);
                db.Entry(obj).Property(x => x.OrderTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.OrderStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.OrderPaymentStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.OrderStatusShipId).IsModified = true;
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.TotalPrice).IsModified = true;
                db.Entry(obj).Property(x => x.TotalShipFee).IsModified = true;
                db.Entry(obj).Property(x => x.Tax).IsModified = true;
                db.Entry(obj).Property(x => x.ShipCountryAddress).IsModified = true;
                db.Entry(obj).Property(x => x.ShipProvinceAddress).IsModified = true;
                db.Entry(obj).Property(x => x.ShipDistrictAddress).IsModified = true;
                db.Entry(obj).Property(x => x.ShipWardAddress).IsModified = true;
                db.Entry(obj).Property(x => x.ShipAddressDetail).IsModified = true;
                db.Entry(obj).Property(x => x.ShipRecipientName).IsModified = true;
                db.Entry(obj).Property(x => x.ShipRecipientPhone).IsModified = true;
                db.Entry(obj).Property(x => x.ShipProvinceAddressId).IsModified = true;
                db.Entry(obj).Property(x => x.ShipDistrictAddressId).IsModified = true;
                db.Entry(obj).Property(x => x.ShipWardAddressId).IsModified = true;
                db.Entry(obj).Property(x => x.FinalPrice).IsModified = true;
                db.Entry(obj).Property(x => x.PromotionId).IsModified = true;
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;
                db.Entry(obj).Property(x => x.LabelId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Order obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Orders.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePermanently(int? objId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the obj for specific obj id
                var obj = await db.Orders.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Orders.Remove(obj);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }

                return result;
            }

            return result;
        }


        public int Count()
        {
            int result = 0;

            if (db != null)
            {
                //Find the obj for specific obj id
                result = (
                    from row in db.Orders
                    where row.Active == 1
                    select row
                ).Count();
            }

            return result;
        }

        public async Task<DTResult<OrdersViewModel>> ListServerSide(OrdersDTParameters parameters)
        {
            //0. Options
            string searchAll = parameters.SearchAll.Trim(); //Trim text
            string orderCritirea = "Id"; //Set default critirea
            int recordTotal, recordFiltered;
            bool orderDirectionASC = true; //Set default ascending
            if (parameters.Order != null)
            {
                orderCritirea = parameters.Columns[parameters.Order[0].Column].Data;
                orderDirectionASC = parameters.Order[0].Dir == DTOrderDir.ASC;
            }

            //1. Join
            var query = from row in db.Orders
                join ot in db.OrderTypes on row.OrderTypeId equals ot.Id
                join os in db.OrderStatuses on row.OrderStatusId equals os.Id
                join ops in db.OrderPaymentStatuses on row.OrderPaymentStatusId equals ops.Id
                join oss in db.OrderStatuses on row.OrderStatusShipId equals oss.Id
                join p in db.Promotions on row.PromotionId equals p.Id into pGroup
                from p in pGroup.DefaultIfEmpty()
                join a in db.Accounts on row.AccountId equals a.Id
                where row.Active == 1
                      && ot.Active == 1
                      && os.Active == 1
                      && ops.Active == 1
                      && oss.Active == 1
                      && a.Active == 1
                select new
                {
                    row,
                    ot,
                    os,
                    ops,
                    oss,
                    a,
                    p
                };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.TotalPrice.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.TotalShipFee.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Tax.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipCountryAddress.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipProvinceAddress.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipDistrictAddress.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipWardAddress.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipAddressDetail.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipRecipientName.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipRecipientPhone.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipProvinceAddressId.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipDistrictAddressId.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ShipWardAddressId.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.FinalPrice.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.AccountId.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.LabelId.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.CreatedTime.ToCustomString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General))
                );
            }

            foreach (var item in parameters.Columns)
            {
                var fillter = item.Search.Value.Trim();
                if (fillter.Length > 0)
                {
                    switch (item.Data)
                    {
                        case "id":
                            query = query.Where(c => c.row.Id.ToString().Trim().Contains(fillter));
                            break;
                        case "active":
                            query = query.Where(c => c.row.Active.ToString().Trim().Contains(fillter));
                            break;
                        case "name":
                            query = query.Where(c => (c.row.Name ?? "").Contains(fillter));
                            break;
                        case "description":
                            query = query.Where(c => (c.row.Description ?? "").Contains(fillter));
                            break;
                        case "info":
                            query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
                            break;
                        case "totalPrice":
                            query = query.Where(c => c.row.TotalPrice.ToString().Trim().Contains(fillter));
                            break;
                        case "totalShipFee":
                            query = query.Where(c => c.row.TotalShipFee.ToString().Trim().Contains(fillter));
                            break;
                        case "tax":
                            query = query.Where(c => c.row.Tax.ToString().Trim().Contains(fillter));
                            break;
                        case "shipCountryAddress":
                            query = query.Where(c => (c.row.ShipCountryAddress ?? "").Contains(fillter));
                            break;
                        case "shipProvinceAddress":
                            query = query.Where(c => (c.row.ShipProvinceAddress ?? "").Contains(fillter));
                            break;
                        case "shipDistrictAddress":
                            query = query.Where(c => (c.row.ShipDistrictAddress ?? "").Contains(fillter));
                            break;
                        case "shipWardAddress":
                            query = query.Where(c => (c.row.ShipWardAddress ?? "").Contains(fillter));
                            break;
                        case "shipAddressDetail":
                            query = query.Where(c => (c.row.ShipAddressDetail ?? "").Contains(fillter));
                            break;
                        case "shipRecipientName":
                            query = query.Where(c => (c.row.ShipRecipientName ?? "").Contains(fillter));
                            break;
                        case "shipRecipientPhone":
                            query = query.Where(c => (c.row.ShipRecipientPhone ?? "").Contains(fillter));
                            break;
                        case "shipProvinceAddressId":
                            query = query.Where(c => c.row.ShipProvinceAddressId.ToString().Trim().Contains(fillter));
                            break;
                        case "shipDistrictAddressId":
                            query = query.Where(c => c.row.ShipDistrictAddressId.ToString().Trim().Contains(fillter));
                            break;
                        case "shipWardAddressId":
                            query = query.Where(c => c.row.ShipWardAddressId.ToString().Trim().Contains(fillter));
                            break;
                        case "finalPrice":
                            query = query.Where(c => c.row.FinalPrice.ToString().Trim().Contains(fillter));
                            break;
                        case "promotionId":
                            query = query.Where(c => c.row.PromotionId.ToString().Trim().Contains(fillter));
                            break;
                        case "labelId":
                            query = query.Where(c => (c.row.LabelId ?? "").Contains(fillter));
                            break;
                        case "createdTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                    .AddDays(1).AddSeconds(-1);
                                query = query.Where(c =>
                                    c.row.CreatedTime >= startDate && c.row.CreatedTime <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.CreatedTime.Date == date.Date);
                            }

                            break;
                    }
                }
            }

            if (parameters.OrderTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.OrderTypeIds.Contains(c.row.OrderType.Id));
            }

            if (parameters.OrderStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.OrderStatusIds.Contains(c.row.OrderStatus.Id));
            }

            if (parameters.OrderPaymentStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.OrderPaymentStatusIds.Contains(c.row.OrderPaymentStatus.Id));
            }

            if (parameters.AccountsIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountsIds.Contains(c.row.Account.Id));
            }

            //3.Query second
            var query2 = query.Select(c => new OrdersViewModel()
            {
                Id = c.row.Id,
                OrderTypeId = c.ot.Id,
                OrderTypeName = c.ot.Name,
                OrderStatusId = c.os.Id,
                OrderStatusName = c.os.Name,
                OrderPaymentStatusId = c.ops.Id,
                OrderPaymentStatusName = c.ops.Name,
                OrderStatusShipId = c.oss.Id,
                OrderStatusShipName = c.oss.Name,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                TotalPrice = c.row.TotalPrice,
                TotalShipFee = c.row.TotalShipFee,
                Tax = c.row.Tax,
                ShipCountryAddress = c.row.ShipCountryAddress,
                ShipProvinceAddress = c.row.ShipProvinceAddress,
                ShipDistrictAddress = c.row.ShipDistrictAddress,
                ShipWardAddress = c.row.ShipWardAddress,
                ShipAddressDetail = c.row.ShipAddressDetail,
                ShipRecipientName = c.row.ShipRecipientName,
                ShipRecipientPhone = c.row.ShipRecipientPhone,
                ShipProvinceAddressId = c.row.ShipProvinceAddressId,
                ShipDistrictAddressId = c.row.ShipDistrictAddressId,
                ShipWardAddressId = c.row.ShipWardAddressId,
                FinalPrice = c.row.FinalPrice,
                PromotionId = c.row.PromotionId,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                AccountPhoto = c.a.Photo,
                AccountPhone = c.a.Phone,
                LabelId = c.row.LabelId,
                CreatedTime = c.row.CreatedTime,
                OrderDetails = (from od in db.OrderDetails
                        join pr in db.Products on od.ProductId equals pr.Id
                        where (od.Active == 1 && pr.Active == 1 && od.OrderId == c.row.Id)
                        select new OrderDetailViewModel
                        {
                            Quantity = od.Quantity,
                            Price = od.Price,
                            FinalPrice = od.FinalPrice == (od.Price * od.Quantity)
                                ? od.FinalPrice
                                : (od.Price * od.Quantity),
                            ProductName = pr.Name,
                            ProductId = pr.Id
                        }
                    ).ToList(),
            });
            //4. Sort
            query2 = query2.OrderByDynamic<OrdersViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<OrdersViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<OrdersViewModel> DetailById(int? id)
        {
            if (db != null)
            {
                var detail = await (from row in db.Orders
                    join ot in db.OrderTypes on row.OrderTypeId equals ot.Id
                    join a in db.Accounts on row.AccountId equals a.Id
                    join os in db.OrderStatuses on row.OrderStatusId equals os.Id
                    join ops in db.OrderPaymentStatuses on row.OrderPaymentStatusId equals ops.Id
                    join oss in db.OrderStatuses on row.OrderStatusShipId equals oss.Id
                    where row.Active == 1 && row.Id == id
                                          && ot.Active == 1
                                          && os.Active == 1
                                          && ops.Active == 1
                                          && oss.Active == 1
                                          && a.Active == 1
                    select new OrdersViewModel
                    {
                        Id = row.Id,
                        OrderTypeId = ot.Id,
                        OrderTypeName = ot.Name,
                        OrderStatusId = os.Id,
                        OrderStatusName = os.Name,
                        OrderPaymentStatusId = ops.Id,
                        OrderPaymentStatusName = ops.Name,
                        OrderStatusShipId = oss.Id,
                        OrderStatusShipName = oss.Name,
                        Active = row.Active,
                        Name = row.Name,
                        Description = row.Description,
                        Info = row.Info,
                        TotalPrice = row.TotalPrice,
                        TotalShipFee = row.TotalShipFee,
                        Tax = row.Tax,
                        ShipCountryAddress = row.ShipCountryAddress,
                        ShipProvinceAddress = row.ShipProvinceAddress,
                        ShipDistrictAddress = row.ShipDistrictAddress,
                        ShipWardAddress = row.ShipWardAddress,
                        ShipAddressDetail = row.ShipAddressDetail,
                        ShipRecipientName = row.ShipRecipientName,
                        ShipRecipientPhone = row.ShipRecipientPhone,
                        ShipProvinceAddressId = row.ShipProvinceAddressId,
                        ShipDistrictAddressId = row.ShipDistrictAddressId,
                        ShipWardAddressId = row.ShipWardAddressId,
                        FinalPrice = row.FinalPrice,
                        PromotionId = row.PromotionId,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        AccountPhone = a.Phone,
                        LabelId = row.LabelId,
                        CreatedTime = row.CreatedTime,
                        OrderDetails = (from od in db.OrderDetails
                                join pr in db.Products on od.ProductId equals pr.Id
                                where (od.Active == 1 && pr.Active == 1 && od.OrderId == row.Id)
                                select new OrderDetailViewModel
                                {
                                    Id = od.Id,
                                    Quantity = od.Quantity,
                                    Price = od.Price,
                                    FinalPrice = od.FinalPrice == (od.Price * od.Quantity)
                                        ? od.FinalPrice
                                        : (od.Price * od.Quantity),
                                    ProductName = pr.Name,
                                    ProductPhoto = pr.Photo,
                                    ProductId = pr.Id
                                }
                            ).ToList()
                    }).FirstOrDefaultAsync();

                return detail;
            }

            return null;
        }

        public async Task<List<OrdersViewModel>> ListById(int? id, int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                var list = await (from row in db.Orders
                    join ot in db.OrderTypes on row.OrderTypeId equals ot.Id
                    join a in db.Accounts on row.AccountId equals a.Id
                    join os in db.OrderStatuses on row.OrderStatusId equals os.Id
                    join ops in db.OrderPaymentStatuses on row.OrderPaymentStatusId equals ops.Id
                    join oss in db.OrderStatuses on row.OrderStatusShipId equals oss.Id
                    where row.Active == 1 && a.Id == id
                                          && ot.Active == 1
                                          && os.Active == 1
                                          && ops.Active == 1
                                          && oss.Active == 1
                                          && a.Active == 1
                    select new OrdersViewModel
                    {
                        Id = row.Id,
                        OrderTypeId = row.Id,
                        OrderTypeName = ot.Name,
                        OrderStatusId = os.Id,
                        OrderStatusName = os.Name,
                        OrderPaymentStatusId = ops.Id,
                        OrderPaymentStatusName = ops.Name,
                        OrderStatusShipId = oss.Id,
                        OrderStatusShipName = oss.Name,
                        Active = row.Active,
                        Name = row.Name,
                        Description = row.Description,
                        Info = row.Info,
                        TotalPrice = row.TotalPrice,
                        TotalShipFee = row.TotalShipFee,
                        Tax = row.Tax,
                        ShipCountryAddress = row.ShipCountryAddress,
                        ShipProvinceAddress = row.ShipProvinceAddress,
                        ShipDistrictAddress = row.ShipDistrictAddress,
                        ShipWardAddress = row.ShipWardAddress,
                        ShipAddressDetail = row.ShipAddressDetail,
                        ShipRecipientName = row.ShipRecipientName,
                        ShipRecipientPhone = row.ShipRecipientPhone,
                        ShipProvinceAddressId = row.ShipProvinceAddressId,
                        ShipDistrictAddressId = row.ShipDistrictAddressId,
                        ShipWardAddressId = row.ShipWardAddressId,
                        FinalPrice = row.FinalPrice,
                        PromotionId = row.PromotionId,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        AccountPhone = a.Phone,
                        LabelId = row.LabelId,
                        CreatedTime = row.CreatedTime,
                        OrderDetails = (from od in db.OrderDetails
                                join pr in db.Products on od.ProductId equals pr.Id
                                where (od.Active == 1 && pr.Active == 1 && od.OrderId == row.Id)
                                select new OrderDetailViewModel
                                {
                                    Id = od.Id,
                                    Quantity = od.Quantity,
                                    Price = od.Price,
                                    FinalPrice = od.FinalPrice == (od.Price * od.Quantity)
                                        ? od.FinalPrice
                                        : (od.Price * od.Quantity),
                                    ProductName = pr.Name,
                                    ProductPhoto = pr.Photo,
                                    ProductId = pr.Id
                                }
                            ).ToList()
                    }).Skip(offSet).Take(pageSize).ToListAsync();

                return list;
            }

            return null;
        }

        public async Task<List<OrdersViewModel>> ListOrderByOrderStatusId(int accountId, int orderStatusId,
            int pageIndex,
            int pageSize)
        {
            if (db != null)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                var result = await (
                    from row in db.Orders
                    join ot in db.OrderTypes on row.OrderTypeId equals ot.Id
                    join a in db.Accounts on row.AccountId equals a.Id
                    join os in db.OrderStatuses on row.OrderStatusId equals os.Id
                    join ops in db.OrderPaymentStatuses on row.OrderPaymentStatusId equals ops.Id
                    join oss in db.OrderStatuses on row.OrderStatusShipId equals oss.Id
                    where row.Active == 1
                          && row.OrderStatusId == orderStatusId
                          && row.AccountId == accountId
                          && ot.Active == 1
                          && os.Active == 1
                          && ops.Active == 1
                          && oss.Active == 1
                          && a.Active == 1
                    select new OrdersViewModel
                    {
                        Id = row.Id,
                        OrderTypeId = row.Id,
                        OrderTypeName = ot.Name,
                        OrderStatusId = os.Id,
                        OrderStatusName = os.Name,
                        OrderPaymentStatusId = ops.Id,
                        OrderPaymentStatusName = ops.Name,
                        OrderStatusShipId = oss.Id,
                        OrderStatusShipName = oss.Name,
                        Active = row.Active,
                        Name = row.Name,
                        Description = row.Description,
                        Info = row.Info,
                        TotalPrice = row.TotalPrice,
                        TotalShipFee = row.TotalShipFee,
                        Tax = row.Tax,
                        ShipCountryAddress = row.ShipCountryAddress,
                        ShipProvinceAddress = row.ShipProvinceAddress,
                        ShipDistrictAddress = row.ShipDistrictAddress,
                        ShipWardAddress = row.ShipWardAddress,
                        ShipAddressDetail = row.ShipAddressDetail,
                        ShipRecipientName = row.ShipRecipientName,
                        ShipRecipientPhone = row.ShipRecipientPhone,
                        ShipProvinceAddressId = row.ShipProvinceAddressId,
                        ShipDistrictAddressId = row.ShipDistrictAddressId,
                        ShipWardAddressId = row.ShipWardAddressId,
                        FinalPrice = row.FinalPrice,
                        PromotionId = row.PromotionId,
                        AccountId = a.Id,
                        AccountName = a.Name,
                        AccountPhone = a.Phone,
                        LabelId = row.LabelId,
                        CreatedTime = row.CreatedTime,
                        OrderDetails = (from od in db.OrderDetails
                                join pr in db.Products on od.ProductId equals pr.Id
                                where (od.Active == 1 && pr.Active == 1 && od.OrderId == row.Id)
                                select new OrderDetailViewModel
                                {
                                    Id = od.Id,
                                    Quantity = od.Quantity,
                                    Price = od.Price,
                                    FinalPrice = od.FinalPrice == (od.Price * od.Quantity)
                                        ? od.FinalPrice
                                        : (od.Price * od.Quantity),
                                    ProductName = pr.Name,
                                    ProductPhoto = pr.Photo,
                                    ProductId = pr.Id
                                }
                            ).ToList()
                    }).Skip(offSet).Take(pageSize).ToListAsync();

                return result;
            }

            return null;
        }

        public DatabaseFacade DataBase()
        {
            return db.Database;
        }

        public async Task DeleteById(int? id, int accountId)
        {
            if (db != null)
            {
                var existingOrder = await db.Orders
                    .FirstOrDefaultAsync(cp => cp.Id == id && cp.AccountId == accountId);
                existingOrder.OrderStatusId = 1000006;
                //Update that obj
                db.Orders.Attach(existingOrder);
                db.Entry(existingOrder).Property(x => x.Active).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> CountListOrderByAccountId(int? accountId, int orderstatusId)
        {
            int result = 0;
            if (db != null)
            {
                result = (
                        from row in db.Orders
                        where (
                            row.Active == 1 && row.AccountId == accountId && row.OrderStatusId == orderstatusId
                        )
                        select row)
                    .Count();
            }

            return result;
        }
        public async Task<List<OrderCountViewModel>> CountListOrders(int accountId)
        {
            if (db != null)
            {
                var orderCounts = await db.Orders
                    .Where(o => o.AccountId == accountId && o.Active == 1)
                    .GroupBy(o => o.OrderStatusId)
                    .Select(g => new OrderCountViewModel
                    {
                        Id = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

                return orderCounts;
            }

            throw new Exception("db is null");
        }
    }
}