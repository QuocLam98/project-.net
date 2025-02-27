
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
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Repository
{
    public class RoleMenuRepository : IRoleMenuRepository
    {
        HomeDoctorContext db;
        public RoleMenuRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<RoleMenu>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.RoleMenus
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<RoleMenu>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.RoleMenus
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<RoleMenu>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.RoleMenus
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<RoleMenu> Detail(int? id)
        {
            if (db != null)
            {
                return await db.RoleMenus.FirstOrDefaultAsync(x => x.Id == id && x.Active == 1);
            }

            return null;
        }


        public async Task<RoleMenu> Add(RoleMenu obj)
        {
            if (db != null)
            {
                await db.RoleMenus.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }
        public async Task<List<RoleMenu>> AddMany(List<RoleMenu> obj)
        {
            if (db != null)
            {
                await db.RoleMenus.AddRangeAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }

        public async Task Update(RoleMenu obj)
        {
            if (db != null)
            {
                //Update that object
                db.RoleMenus.Attach(obj);
                db.Entry(obj).Property(x => x.RoleId).IsModified = true;
                db.Entry(obj).Property(x => x.MenuId).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(RoleMenu obj)
        {
            if (db != null)
            {
                //Update that obj
                db.RoleMenus.Attach(obj);
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
                var obj = await db.RoleMenus.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.RoleMenus.Remove(obj);

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
                    from row in db.RoleMenus
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<RoleMenuViewModel>> ListServerSide(RoleMenuDTParameters parameters)
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
            var query = from row in db.RoleMenus

                        join r in db.Roles on row.RoleId equals r.Id
                        join m in db.Menus on row.MenuId equals m.Id

                        where row.Active == 1
                                        && r.Active == 1
    && m.Active == 1

                        select new
                        {
                            row,
                            r,
                            m
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

            if (parameters.RoleIds.Count > 0)
            {
                query = query.Where(c => parameters.RoleIds.Contains(c.row.Role.Id));
            }


            if (parameters.MenuIds.Count > 0)
            {
                query = query.Where(c => parameters.MenuIds.Contains(c.row.Menu.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new RoleMenuViewModel()
            {
                Id = c.row.Id,
                RoleId = c.r.Id,
                RoleName = c.r.Name,
                MenuId = c.m.Id,
                MenuName = c.m.Name,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<RoleMenuViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<RoleMenuViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }
        public async Task<List<RoleMenu>> ListByRoleId(int? roleId)
        {
            if (db != null)
            {
                return await (
                    from row in db.RoleMenus
                    where (row.Active == 1 && row.RoleId == roleId)
                    select row)
                    .ToListAsync();
            }

            return null;
        }
    }
}


