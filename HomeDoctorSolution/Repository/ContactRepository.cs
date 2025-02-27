
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
            public class ContactRepository: IContactRepository
                {
                    HomeDoctorContext db;
                    public ContactRepository(HomeDoctorContext _db)
                    {
                        db = _db;
                    }


            public async Task <List<Contact>> List()
            {
                            if(db != null)
                    {
                        return await(
                            from row in db.Contacts
                        where(row.Active == 1)
                        orderby row.Id descending
                        select row
                        ).ToListAsync();
                    }

                    return null;
                }


            public async Task <List< Contact>> Search(string keyword)
            {
                if(db != null)
                {
                    return await(
                        from row in db.Contacts
                                    where(row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                                    orderby row.Id descending
                                    select row
                    ).ToListAsync();
                }
                return null;
            }


            public async Task <List<Contact>> ListPaging(int pageIndex, int pageSize)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                if (db != null) {
                    return await(
                        from row in db.Contacts
                                    where(row.Active == 1)
                                    orderby row.Id descending
                                    select row
                    ).Skip(offSet).Take(pageSize).ToListAsync();
                }
                return null;
            }


            public async Task <Contact> Detail(int ? id)
            {
                if (db != null) {
                    return await db.Contacts.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
                }
                return null;
            }


            public async Task <Contact> Add(Contact obj)
            {
                if (db != null) {
                    await db.Contacts.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return obj;
                }
                return null;
            }


            public async Task Update(Contact obj)
            {
                if (db != null) {
                    //Update that object
                    db.Contacts.Attach(obj);
                    db.Entry(obj).Property(x => x.Active).IsModified = true;
db.Entry(obj).Property(x => x.ContactStatusId).IsModified = true;
db.Entry(obj).Property(x => x.Name).IsModified = true;
db.Entry(obj).Property(x => x.Email).IsModified = true;
db.Entry(obj).Property(x => x.Phone).IsModified = true;
db.Entry(obj).Property(x => x.Message).IsModified = true;
db.Entry(obj).Property(x => x.Description).IsModified = true;

                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
            }


            public async Task Delete(Contact obj)
            {
                if (db != null) {
                    //Update that obj
                    db.Contacts.Attach(obj);
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
                    var obj = await db.Contacts.FirstOrDefaultAsync(x => x.Id == objId);

                    if (obj != null) {
                        //Delete that obj
                        db.Contacts.Remove(obj);

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
                        from row in db.Contacts
                                    where row.Active == 1
                                    select row
                                ).Count();
                }

                return result;
            }
            public async Task <DTResult<ContactViewModel>> ListServerSide(ContactDTParameters parameters)
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
                var query = from row in db.Contacts 

                                    join cs in db.ContactStatuses on row.ContactStatusId equals cs.Id 

                    where row.Active == 1
                                    && cs.Active == 1

                    select new {
                        row,cs
                    };
                
                recordTotal = await query.CountAsync();
                //2. Fillter
                if (!String.IsNullOrEmpty(searchAll)) {
                    searchAll = searchAll.ToLower();
                    query = query.Where(c =>
                        EF.Functions.Collate(c.row.Id.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Email.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Phone.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Message.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
 case "email":
                query = query.Where(c => (c.row.Email ?? "").Contains(fillter));
                break;
 case "phone":
                query = query.Where(c => (c.row.Phone ?? "").Contains(fillter));
                break;
 case "message":
                query = query.Where(c => (c.row.Message ?? "").Contains(fillter));
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
                
                                if (parameters.ContactStatusIds.Count > 0)
                                {
                                    query = query.Where(c => parameters.ContactStatusIds.Contains(c.row.ContactStatus.Id));
                                }
                                

                //3.Query second
                var query2 = query.Select(c => new ContactViewModel()
                {
                    Id = c.row.Id,
Active = c.row.Active,
ContactStatusId = c.cs.Id,
ContactStatusName = c.cs.Name,
Name = c.row.Name,
Email = c.row.Email,
Phone = c.row.Phone,
Message = c.row.Message,
Description = c.row.Description,
CreatedTime = c.row.CreatedTime,

                });
                //4. Sort
                query2 = query2.OrderByDynamic<ContactViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
                recordFiltered = await query2.CountAsync();
                //5. Return data
                return new DTResult<ContactViewModel>()
                {
                    data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                        draw = parameters.Draw,
                        recordsFiltered = recordFiltered,
                        recordsTotal = recordTotal
                };
            }
        }
    }


