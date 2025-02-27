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
using HomeDoctor.Models.ModelDTO;

namespace HomeDoctorSolution.Repository
{
    public class CartRepository : ICartRepository
    {
        HomeDoctorContext db;

        public CartRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Cart>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Carts
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Cart>> Search(string keyword)
        {
            if (db != null)
            {
                int accountId;
                if (int.TryParse(keyword, out accountId))
                {
                    return await (
                        from row in db.Carts
                        where row.Active == 1 && row.AccountId == accountId
                        orderby row.Id descending
                        select row
                    ).ToListAsync();
                }

                return new List<Cart>();
            }

            return null;
        }


        public async Task<List<Cart>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Carts
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }


        public async Task<Cart> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Carts.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }

            return null;
        }


        public async Task<Cart> Add(Cart obj)
        {
            if (db != null)
            {
                await db.Carts.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }

            return null;
        }


        public async Task Update(Cart obj)
        {
            if (db != null)
            {
                //Update that object
                db.Carts.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Cart obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Carts.Attach(obj);
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
                // Find the cart for the specific cartId
                var cart = await db.Carts
                    .Include(c => c.CartProducts) // Include CartProducts
                    .FirstOrDefaultAsync(x => x.Id == objId);

                if (cart != null)
                {
                    // Remove all CartProducts associated with the Cart
                    db.CartProducts.RemoveRange(cart.CartProducts);

                    // Remove the Cart itself
                    db.Carts.Remove(cart);

                    // Commit the transaction
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
                    from row in db.Carts
                    where row.Active == 1
                    select row
                ).Count();
            }

            return result;
        }

        public async Task<DTResult<CartViewModel>> ListServerSide(CartDTParameters parameters)
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
            var query = from row in db.Carts
                join a in db.Accounts on row.AccountId equals a.Id
                where row.Active == 1
                      && a.Active == 1
                select new
                {
                    row,
                    a
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

            if (parameters.AccountIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountIds.Contains(c.row.Account.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new CartViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<CartViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<CartViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<List<CartDetailViewModel>> GetProductsInCart(int? cartId)
        {
            if (db != null)
            {
                var cartProducts = await db.Carts
                    .Where(c => c.Active == 1 && c.Id == cartId)
                    .SelectMany(c => c.CartProducts)
                    .Where(cp => cp.Active == 1)
                    .Select(cp => new CartDetailViewModel
                    {
                        CartId = cp.CartId,
                        ProductId = cp.Product.Id,
                        Name = cp.Product.Name,
                        Price = cp.Product.Price,
                        Photo = cp.Product.Photo,
                        Quantity = cp.Quantity
                    })
                    .ToListAsync();

                return cartProducts;
            }

            return null;
        }

        public async Task<Cart> CartInfo(int? id)
        {
            if (db != null)
            {
                var cart = await db.Carts
                    .FirstOrDefaultAsync(c => c.Active == 1 && c.AccountId == id);

                if (cart != null)
                {
                    return cart;
                }
            }

            return null;
        }
    }
}