
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

    namespace HomeDoctorSolution.Repository
        {
            public class OrganizationRepository: IOrganizationRepository
                {
                    HomeDoctorContext db;
                    public OrganizationRepository(HomeDoctorContext _db)
                    {
                        db = _db;
                    }


            public async Task <List<Organization>> List()
            {
                            if(db != null)
                    {
                        return await(
                            from row in db.Organizations
                        where(row.Active == 1)
                        orderby row.Id descending
                        select row
                        ).ToListAsync();
                    }

                    return null;
                }


            public async Task <List< Organization>> Search(string keyword)
            {
                if(db != null)
                {
                    return await(
                        from row in db.Organizations
                                    where(row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                                    orderby row.Id descending
                                    select row
                    ).ToListAsync();
                }
                return null;
            }


            public async Task <List<Organization>> ListPaging(int pageIndex, int pageSize)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                if (db != null) {
                    return await(
                        from row in db.Organizations
                                    where(row.Active == 1)
                                    orderby row.Id descending
                                    select row
                    ).Skip(offSet).Take(pageSize).ToListAsync();
                }
                return null;
            }


            public async Task <Organization> Detail(int ? id)
            {
                if (db != null) {
                    return await db.Organizations.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
                }
                return null;
            }


            public async Task <Organization> Add(Organization obj)
            {
                if (db != null) {
                    await db.Organizations.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return obj;
                }
                return null;
            }


            public async Task Update(Organization obj)
            {
                if (db != null) {
                    //Update that object
                    db.Organizations.Attach(obj);
                    db.Entry(obj).Property(x => x.OrganizationTypeId).IsModified = true;
db.Entry(obj).Property(x => x.OrganizationStatusId).IsModified = true;
db.Entry(obj).Property(x => x.Active).IsModified = true;
db.Entry(obj).Property(x => x.Name).IsModified = true;
db.Entry(obj).Property(x => x.Description).IsModified = true;
db.Entry(obj).Property(x => x.Text).IsModified = true;
db.Entry(obj).Property(x => x.Phone).IsModified = true;
db.Entry(obj).Property(x => x.Address).IsModified = true;
db.Entry(obj).Property(x => x.Photo).IsModified = true;

                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
            }


            public async Task Delete(Organization obj)
            {
                if (db != null) {
                    //Update that obj
                    db.Organizations.Attach(obj);
                    db.Entry(obj).Property(x => x.Active).IsModified = true;

                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
            }

            public async Task<int> DeletePermanently(int ? objId)
            {
                            int result = 0;

                if (db != null) {
                    //Find the obj for specific obj id
                    var obj = await db.Organizations.FirstOrDefaultAsync(x => x.Id == objId);

                    if (obj != null) {
                        //Delete that obj
                        db.Organizations.Remove(obj);

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

                if (db != null) {
                    //Find the obj for specific obj id
                    result = (
                        from row in db.Organizations
                                    where row.Active == 1
                                    select row
                                ).Count();
                }

                return result;
            }
            public async Task <DTResult<OrganizationViewModel>> ListServerSide(OrganizationDTParameters parameters)
            {
                //0. Options
                string searchAll = parameters.SearchAll.Trim();//Trim text
                string orderCritirea = "Id";//Set default critirea
                int recordTotal, recordFiltered;
                bool orderDirectionASC = true;//Set default ascending
                if (parameters.Order != null) {
                    orderCritirea = parameters.Columns[parameters.Order[0].Column].Data;
                    orderDirectionASC = parameters.Order[0].Dir == DTOrderDir.ASC;
                }
                //1. Join
                var query = from row in db.Organizations 

                                    join ot in db.OrganizationTypes on row.OrganizationTypeId equals ot.Id 
join os in db.OrganizationStatuses on row.OrganizationStatusId equals os.Id 

                    where row.Active == 1
                                    && ot.Active == 1
&& os.Active == 1

                    select new {
                        row,ot,os
                    };
                
                recordTotal = await query.CountAsync();
                //2. Fillter
                if (!String.IsNullOrEmpty(searchAll)) {
                    searchAll = searchAll.ToLower();
                    query = query.Where(c =>
                        EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Text.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Phone.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Address.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.CreatedTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General))

                    );
                }
                foreach(var item in parameters.Columns)
                {
                    var fillter = item.Search.Value.Trim();
                    if (fillter.Length > 0) {
                        switch (item.Data) {
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
 case "text":
                query = query.Where(c => (c.row.Text ?? "").Contains(fillter));
                break;
 case "phone":
                query = query.Where(c => (c.row.Phone ?? "").Contains(fillter));
                break;
 case "address":
                query = query.Where(c => (c.row.Address ?? "").Contains(fillter));
                break;
 case "photo":
                query = query.Where(c => (c.row.Photo ?? "").Contains(fillter));
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
                
                                if (parameters.OrganizationTypeIds.Count > 0)
                                {
                                    query = query.Where(c => parameters.OrganizationTypeIds.Contains(c.row.OrganizationType.Id));
                                }
                                

                                if (parameters.OrganizationStatusIds.Count > 0)
                                {
                                    query = query.Where(c => parameters.OrganizationStatusIds.Contains(c.row.OrganizationStatus.Id));
                                }
                                

                //3.Query second
                var query2 = query.Select(c => new OrganizationViewModel()
                {
                    Id = c.row.Id,
OrganizationTypeId = c.ot.Id,
OrganizationTypeName = c.ot.Name,
OrganizationStatusId = c.os.Id,
OrganizationStatusName = c.os.Name,
Active = c.row.Active,
Name = c.row.Name,
Description = c.row.Description,
Text = c.row.Text,
Phone = c.row.Phone,
Address = c.row.Address,
Photo = c.row.Photo,
CreatedTime = c.row.CreatedTime,

                });
                //4. Sort
                query2 = query2.OrderByDynamic<OrganizationViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
                recordFiltered = await query2.CountAsync();
                //5. Return data
                return new DTResult<OrganizationViewModel>()
                {
                    data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                        draw = parameters.Draw,
                        recordsFiltered = recordFiltered,
                        recordsTotal = recordTotal
                };
            }
        }
    }


