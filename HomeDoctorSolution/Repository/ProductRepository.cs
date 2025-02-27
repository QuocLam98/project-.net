
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
using HomeDoctorSolution.Util.Entities;
using HomeDoctor.Models.ModelDTO;
using HomeDoctor.Models.ViewModels.Product;

namespace HomeDoctorSolution.Repository
{
    public class ProductRepository : IProductRepository
    {
        HomeDoctorContext db;
        public ProductRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Product>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Products
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }
        public async Task<List<Product>> listBylistId(IEnumerable<int> listId)
        {
            if (db != null)
            {
                return await (
                    from row in db.Products
                    where (row.Active == 1 && listId.Contains(row.Id))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }

        public async Task<List<Product>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Products
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<Product>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Products
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<Product> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Products.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }


        public async Task<Product> Add(Product obj)
        {
            if (db != null)
            {
                await db.Products.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(Product obj)
        {
            if (db != null)
            {
                //Update that object
                db.Products.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = false;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Info).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.Quantity).IsModified = true;
                db.Entry(obj).Property(x => x.Price).IsModified = true;
                db.Entry(obj).Property(x => x.ProductCategoryId).IsModified = true;
                db.Entry(obj).Property(x => x.ProductBrandId).IsModified = true;
                db.Entry(obj).Property(x => x.ProductTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.ProductStatusId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Product obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Products.Attach(obj);
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
                var obj = await db.Products.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Products.Remove(obj);

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
                    from row in db.Products
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<ProductViewModel>> ListServerSide(ProductDTParameters parameters)
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
            var query = from row in db.Products

                        join pc in db.ProductCategories on row.ProductCategoryId equals pc.Id
                        join pb in db.ProductBrands on row.ProductBrandId equals pb.Id
                        join pt in db.ProductTypes on row.ProductTypeId equals pt.Id
                        join ps in db.ProductStatuses on row.ProductStatusId equals ps.Id

                        where row.Active == 1
                                        && pc.Active == 1
    && pb.Active == 1
    && pt.Active == 1
    && ps.Active == 1

                        select new
                        {
                            row,
                            pc,
                            pb,
                            pt,
                            ps
                        };

            recordTotal = await query.CountAsync();
            //2. Fillter
            if (!String.IsNullOrEmpty(searchAll))
            {
                searchAll = searchAll.ToLower();
                query = query.Where(c =>
                    EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Quantity.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Price.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                        case "photo":
                            query = query.Where(c => (c.row.Photo ?? "").Contains(fillter));
                            break;
                        case "quantity":
                            query = query.Where(c => c.row.Quantity.ToString().Trim().Contains(fillter));
                            break;
                        case "price":
                            query = query.Where(c => c.row.Price.ToString().Trim().Contains(fillter));
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

            if (parameters.ProductCategoryIds.Count > 0)
            {
                query = query.Where(c => parameters.ProductCategoryIds.Contains(c.row.ProductCategory.Id));
            }


            if (parameters.ProductBrandIds.Count > 0)
            {
                query = query.Where(c => parameters.ProductBrandIds.Contains(c.row.ProductBrand.Id));
            }


            if (parameters.ProductTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.ProductTypeIds.Contains(c.row.ProductType.Id));
            }


            if (parameters.ProductStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.ProductStatusIds.Contains(c.row.ProductStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new ProductViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Info = c.row.Info,
                Photo = c.row.Photo,
                Quantity = c.row.Quantity,
                Price = c.row.Price,
                ProductCategoryId = c.pc.Id,
                ProductCategoryName = c.pc.Name,
                ProductBrandId = c.pb.Id,
                ProductBrandName = c.pb.Name,
                ProductTypeId = c.pt.Id,
                ProductTypeName = c.pt.Name,
                ProductStatusId = c.ps.Id,
                ProductStatusName = c.ps.Name,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<ProductViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<ProductViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public Task<PagingData<List<ProductViewModel>>> ListProductByName(ProductParameters parameter)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> ListProduct()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetListProductByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameExist(int id, string name)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewModel> DetailProductVM(int id)
        {
            if (db != null)
            {
                return await (from p in db.Products
                              join pb in db.ProductBrands on p.ProductBrandId equals pb.Id
                              join pt in db.ProductTypes on p.ProductTypeId equals pt.Id
                              join ps in db.ProductStatuses on p.ProductStatusId equals ps.Id
                              join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                              where p.Active == 1 && pb.Active == 1 && pt.Active == 1 && ps.Active == 1 && pc.Active == 1
                              && p.Id == id
                              select new ProductViewModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  ProductBrandName = pb.Name,
                                  ProductCategoryName = pc.Name,
                                  ProductTypeName = pt.Name,
                                  ProductStatusName = ps.Name,
                                  Info = p.Info,
                                  Description = p.Description,
                                  ShortDescription = p.ShortDescription,
                                  Quantity = p.Quantity,
                                  Price = p.Price,
                                  PromotionPrice = p.PromotionPrice,
                                  Photo = p.Photo,
                                  ListPhotos = db.ProductMetas.Where(c => c.Active == 1 && c.Key == "PRODUCT_PHOTO" && c.ProductId == id).Select(c => new ListPhoto { Photo = c.Value, Description = c.Description }).ToList(),
                                  CreatedTime = p.CreatedTime
                              }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Product>> ListProductBrand(int Id)
        {
            if (db != null)
            {
                var IdBrand = db.Products.Where(x => x.Id == Id && x.Active == 1).Select(x => x.ProductBrandId).FirstOrDefault();
                return await (
                    from row in db.Products
                    where (row.Active == 1 && row.ProductBrandId == IdBrand)
                    orderby row.Id descending
                    select row
                ).Skip(0).Take(4).ToListAsync();
            }

            return null;
        }
        public async Task<List<ProductViewModel>> ListFourProductByTimeDesc()
        {
            if (db != null)
            {
                return await (from row in db.Products

                              join pc in db.ProductCategories on row.ProductCategoryId equals pc.Id
                              join pb in db.ProductBrands on row.ProductBrandId equals pb.Id
                              join pt in db.ProductTypes on row.ProductTypeId equals pt.Id
                              join ps in db.ProductStatuses on row.ProductStatusId equals ps.Id

                              where row.Active == 1 && pc.Active == 1 && pb.Active == 1 && pt.Active == 1 && ps.Active == 1
                              orderby row.CreatedTime descending
                              select new ProductViewModel
                              {
                                  Id = row.Id,
                                  Name = row.Name,
                                  Description = row.Description,
                                  Info = row.Info,
                                  Photo = row.Photo,
                                  Quantity = row.Quantity,
                                  Price = row.Price,
                                  ProductCategoryId = pc.Id,
                                  ProductCategoryName = pc.Name,
                                  ProductBrandId = pb.Id,
                                  ProductBrandName = pb.Name,
                                  ProductTypeId = pt.Id,
                                  ProductTypeName = pt.Name,
                                  ProductStatusId = ps.Id,
                                  ProductStatusName = ps.Name,
                                  CreatedTime = row.CreatedTime,
                              }).Take(4).ToListAsync();
            }
            return null;
        }

        public async Task<PagingData<List<Product>>> SearchProductHomePageAsync(SearchingProductParameters parameter)
        {
            var keyword = parameter.Keywords;
            var pageIndex = parameter.PageIndex;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;
            var categoryId = parameter.CategoryProductId;

            if (parameter.OrderCriteria != null)
            {
                orderCriteria = parameter.OrderCriteria;
                orderAscendingDirection = parameter.OrderCriteria.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = (
                from row in db.Products
                join pc in db.PostCategories on row.ProductCategoryId equals pc.Id
                where row.Active == 1 && pc.Active == 1
                select row
                );
            var allRecord = await result.CountAsync();

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(s =>
                EF.Functions.Collate(s.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)));
            }

            if (categoryId.Length != 0)
            {
                var categoryResult = categoryId.Split(",").Select(n => Int64.Parse(n));
                result = result.Where(r => categoryResult.Contains(r.ProductCategoryId));
                //result = result.Where(s => s.CompanySizeId.ToString().RemoveVietnamese().ToLower().Contains(companySize.RemoveVietnamese().ToLower()));
            }

            //orderby
            if (parameter.OrderCriteria != null)
            {
                switch (parameter.OrderCriteria)
                {
                    case "asc":
                        result = result.OrderBy(x => x.Id);
                        break;
                    case "desc":
                        result = result.OrderByDescending(x => x.Id);
                        break;
                }
            }
            var recordsTotal = await result.CountAsync();
            return new PagingData<List<Product>>
            {
                DataSource = await result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }
    }
}


