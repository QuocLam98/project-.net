using HomeDoctor.Models;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Repository.Interfaces;
using HomeDoctor.Util.DTParameters;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HomeDoctor.Repository
{
    public class ProductMetaRepository : IProductMetaRepository
    {
        HomeDoctorContext db;
        public ProductMetaRepository(HomeDoctorContext _db)
        {
            db = _db;
        }

        public async Task<ProductMeta> Add(ProductMeta ProductMeta)
        {
            if (db != null)
            {
                await db.ProductMetas.AddAsync(ProductMeta);
                await db.SaveChangesAsync();
                return ProductMeta;
            }
            return null;
        }

        public int Count()
        {
            int result = 0;
            if (db != null)
            {
                result = (
                        from row in db.ProductMetas
                        where row.Active == 1
                        select row
                    ).Count();
            }
            return result;
        }

        public async Task Delete(ProductMeta ProductMeta)
        {
            if (db != null)
            {
                db.ProductMetas.Attach(ProductMeta);
                db.Entry(ProductMeta).Property(x => x.Active).IsModified = true;

                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePermanently(int? objId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the obj for specific obj id
                var obj = await db.ProductMetas.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.ProductMetas.Remove(obj);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<ProductMeta> Detail(int? id)
        {
            if (db != null)
            {
                return await db.ProductMetas.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }

        public async Task<List<ProductMeta>> List()
        {
            if (db != null)
            {
                return await(
                    from row in db.ProductMetas
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }

        public async Task<List<ProductMeta>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await(
                    from row in db.ProductMetas
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }

        public async Task<DTResult<ProductMetaViewModel>> ListServerSide(ProductMetaDTParameters parameters)
        {
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
            var query = from row in db.ProductMetas
                        where row.Active == 1
                        select new
                        {
                            row,
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
                    EF.Functions.Collate(c.row.Key.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Value.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                        case "active":
                            query = query.Where(c => c.row.Active.ToString().Trim().Contains(fillter));
                            break;
                        case "name":
                            query = query.Where(c => (c.row.Name ?? "").Contains(fillter));
                            break;
                        case "key":
                            query = query.Where(c => (c.row.Key ?? "").Contains(fillter));
                            break;
                        case "value":
                            query = query.Where(c => (c.row.Value ?? "").Contains(fillter));
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

            //3.Query second
            var query2 = query.Select(c => new ProductMetaViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                Name = c.row.Name,
                Key = c.row.Key,
                Value = c.row.Value,
                Description = c.row.Description,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<ProductMetaViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<ProductMetaViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<List<ProductMeta>> Search(string keyword)
        {
            if (db != null)
            {
                return await(
                    from row in db.ProductMetas
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }

        public async Task Update(ProductMeta obj)
        {
            if (db != null)
            {
                //Update that object
                db.ProductMetas.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Key).IsModified = true;
                db.Entry(obj).Property(x => x.Value).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
