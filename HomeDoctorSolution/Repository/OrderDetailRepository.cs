
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
using NuGet.Versioning;
using System.Collections;
using HomeDoctor.Models.ModelDTO;

namespace HomeDoctorSolution.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        HomeDoctorContext db;
        public OrderDetailRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<OrderDetail>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.OrderDetails
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<OrderDetail>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.OrderDetails
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<OrderDetail>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.OrderDetails
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<OrderDetail> Detail(int? id)
        {
            if (db != null)
            {
                return await db.OrderDetails.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }


        public async Task<OrderDetail> Add(OrderDetail obj)
        {
            if (db != null)
            {
                await db.OrderDetails.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(OrderDetail obj)
        {
            if (db != null)
            {
                //Update that object
                db.OrderDetails.Attach(obj);
                db.Entry(obj).Property(x => x.OrderId).IsModified = true;
                db.Entry(obj).Property(x => x.ProductId).IsModified = true;
                db.Entry(obj).Property(x => x.OrderDetailStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.Quantity).IsModified = true;
                db.Entry(obj).Property(x => x.Price).IsModified = true;
                db.Entry(obj).Property(x => x.FinalPrice).IsModified = true;
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(OrderDetail obj)
        {
            if (db != null)
            {
                //Update that obj
                db.OrderDetails.Attach(obj);
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
                var obj = await db.OrderDetails.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.OrderDetails.Remove(obj);

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
                    from row in db.OrderDetails
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<OrderDetailViewModel>> ListServerSide(OrderDetailDTParameters parameters)
        {
            //0. Options
            string searchAll = parameters.SearchAll.Trim();//Trim text
            string orderCritirea = "Id";//Set default critirea
            int recordTotal, recordFiltered;
            bool orderDirectionASC = true;//Set default ascending
            if (parameters.Order != null)
            {
                orderCritirea = parameters.Columns[parameters.Order[0].Column].Data;
                orderDirectionASC = parameters.Order[0].Dir == DTOrderDir.ASC;
            }
            //1. Join
            var query = from row in db.OrderDetails

                        join p in db.Products on row.ProductId equals p.Id
                        join ods in db.OrderDetailStatuses on row.OrderDetailStatusId equals ods.Id

                        where row.Active == 1
                                        && p.Active == 1
    && ods.Active == 1

                        select new
                        {
                            row,
                            p,
                            ods
                        };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.OrderId.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Quantity.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Price.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.FinalPrice.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.CreatedTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General))

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
                        case "orderId":
                            query = query.Where(c => c.row.OrderId.ToString().Trim().Contains(fillter));
                            break;
                        case "quantity":
                            query = query.Where(c => c.row.Quantity.ToString().Trim().Contains(fillter));
                            break;
                        case "price":
                            query = query.Where(c => c.row.Price.ToString().Trim().Contains(fillter));
                            break;
                        case "finalPrice":
                            query = query.Where(c => c.row.FinalPrice.ToString().Trim().Contains(fillter));
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
                        case "createdTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.CreatedTime >= startDate && c.row.CreatedTime <= endDate);
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

            if (parameters.ProductIds.Count > 0)
            {
                query = query.Where(c => parameters.ProductIds.Contains(c.row.Product.Id));
            }


            if (parameters.OrderDetailStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.OrderDetailStatusIds.Contains(c.row.OrderDetailStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new OrderDetailViewModel()
            {
                Id = c.row.Id,
                OrderId = c.row.OrderId,
                ProductId = c.p.Id,
                ProductName = c.p.Name,
                OrderDetailStatusId = c.ods.Id,
                OrderDetailStatusName = c.ods.Name,
                Quantity = c.row.Quantity,
                Price = c.row.Price,
                FinalPrice = c.row.FinalPrice,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<OrderDetailViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<OrderDetailViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<List<OrderDetail>> AddMany(List<OrderDetail> orderDetails)
        {
            if (db != null)
            {
                await db.OrderDetails.AddRangeAsync(orderDetails);
                await db.SaveChangesAsync();
            }
            return null;
        }
        public async Task<List<OrderDetail>> UpdateMany(List<OrderDetail> orderDetails)
        {
            if (db != null)
            {
                foreach (var obj in orderDetails)
                {
                    db.OrderDetails.Attach(obj);
                    db.Entry(obj).Property(x => x.OrderId).IsModified = true;
                    db.Entry(obj).Property(x => x.ProductId).IsModified = true;
                    db.Entry(obj).Property(x => x.OrderDetailStatusId).IsModified = true;
                    db.Entry(obj).Property(x => x.Quantity).IsModified = true;
                    db.Entry(obj).Property(x => x.Price).IsModified = true;
                    db.Entry(obj).Property(x => x.FinalPrice).IsModified = true;
                    db.Entry(obj).Property(x => x.Active).IsModified = true;
                    db.Entry(obj).Property(x => x.Name).IsModified = true;
                    db.Entry(obj).Property(x => x.Description).IsModified = true;
                }
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<OrderDetail>> ListByOrderId(int orderId)
        {
            if (db != null)
            {
                return await db.OrderDetails.Where(x=> x.OrderId == orderId && x.Active == 1).AsNoTracking().ToListAsync();
            }
            return null;
        }
        public async Task<List<OrderDetail>> CompareAndUpdate(List<OrderDetail> listNew, List<OrderDetail> listOld)
        {
            if (db != null)
            {
                var missingOrderDetail = listOld.Except(listNew, new OrderDetailCompare());
                if(missingOrderDetail != null)
                {
                    // Display the result
                    foreach (var item in missingOrderDetail)
                    {
                        item.Active = 0;
                    }
                    await UpdateMany(missingOrderDetail);
                }
            }
            return null;
        }

        private async Task UpdateMany(IEnumerable<OrderDetail> missingOrderDetail)
        {
            if (db != null)
            {
                foreach (var obj in missingOrderDetail)
                {
                    db.OrderDetails.Attach(obj);
                    db.Entry(obj).Property(x => x.OrderId).IsModified = true;
                    db.Entry(obj).Property(x => x.ProductId).IsModified = true;
                    db.Entry(obj).Property(x => x.OrderDetailStatusId).IsModified = true;
                    db.Entry(obj).Property(x => x.Quantity).IsModified = true;
                    db.Entry(obj).Property(x => x.Price).IsModified = true;
                    db.Entry(obj).Property(x => x.FinalPrice).IsModified = true;
                    db.Entry(obj).Property(x => x.Active).IsModified = true;
                    db.Entry(obj).Property(x => x.Name).IsModified = true;
                    db.Entry(obj).Property(x => x.Description).IsModified = true;
                }
                await db.SaveChangesAsync();
            }
        }
    }
}


