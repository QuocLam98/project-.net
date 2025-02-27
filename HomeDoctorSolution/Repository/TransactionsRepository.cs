
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
            public class TransactionsRepository: ITransactionsRepository
                {
                    HomeDoctorContext db;
                    public TransactionsRepository(HomeDoctorContext _db)
                    {
                        db = _db;
                    }


            public async Task <List<Transaction>> List()
            {
                            if(db != null)
                    {
                        return await(
                            from row in db.Transaction
                        where(row.Active == 1)
                        orderby row.Id descending
                        select row
                        ).ToListAsync();
                    }

                    return null;
                }


            public async Task <List< Transaction>> Search(string keyword)
            {
                if(db != null)
                {
                    return await(
                        from row in db.Transaction
                                    where(row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                                    orderby row.Id descending
                                    select row
                    ).ToListAsync();
                }
                return null;
            }


            public async Task <List<Transaction>> ListPaging(int pageIndex, int pageSize)
            {
                int offSet = 0;
                offSet = (pageIndex - 1) * pageSize;
                if (db != null) {
                    return await(
                        from row in db.Transaction
                                    where(row.Active == 1)
                                    orderby row.Id descending
                                    select row
                    ).Skip(offSet).Take(pageSize).ToListAsync();
                }
                return null;
            }


            public async Task <Transaction> Detail(int ? id)
            {
                if (db != null) {
                    return await db.Transaction.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
                }
                return null;
            }


            public async Task <Transaction> Add(Transaction obj)
            {
                if (db != null) {
                    await db.Transaction.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return obj;
                }
                return null;
            }


            public async Task Update(Transaction obj)
            {
                if (db != null) {
                    //Update that object
                    db.Transaction.Attach(obj);
                    db.Entry(obj).Property(x => x.TransactionTypeId).IsModified = true;
db.Entry(obj).Property(x => x.TransactionStatusId).IsModified = true;
db.Entry(obj).Property(x => x.Active).IsModified = true;
db.Entry(obj).Property(x => x.Name).IsModified = true;
db.Entry(obj).Property(x => x.Description).IsModified = true;
db.Entry(obj).Property(x => x.Info).IsModified = true;
db.Entry(obj).Property(x => x.Amount).IsModified = true;
db.Entry(obj).Property(x => x.SenderInfo).IsModified = true;
db.Entry(obj).Property(x => x.ReceiveInfo).IsModified = true;
db.Entry(obj).Property(x => x.OrderId).IsModified = true;

                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
            }


            public async Task Delete(Transaction obj)
            {
                if (db != null) {
                    //Update that obj
                    db.Transaction.Attach(obj);
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
                    var obj = await db.Transaction.FirstOrDefaultAsync(x => x.Id == objId);

                    if (obj != null) {
                        //Delete that obj
                        db.Transaction.Remove(obj);

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
                        from row in db.Transaction
                                    where row.Active == 1
                                    select row
                                ).Count();
                }

                return result;
            }
            public async Task <DTResult<TransactionsViewModel>> ListServerSide(TransactionsDTParameters parameters)
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
                var query = from row in db.Transaction 

                                    join tt in db.TransactionTypes on row.TransactionTypeId equals tt.Id 
join ts in db.TransactionStatuses on row.TransactionStatusId equals ts.Id 

                    where row.Active == 1
                                    && tt.Active == 1
&& ts.Active == 1

                    select new {
                        row,tt,ts
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
EF.Functions.Collate(c.row.Info.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Amount.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.SenderInfo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.ReceiveInfo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.OrderId.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
 case "info":
                query = query.Where(c => (c.row.Info ?? "").Contains(fillter));
                break;
case "amount":
                        query = query.Where(c => c.row.Amount.ToString().Trim().Contains(fillter));
                        break;
 case "senderInfo":
                query = query.Where(c => (c.row.SenderInfo ?? "").Contains(fillter));
                break;
 case "receiveInfo":
                query = query.Where(c => (c.row.ReceiveInfo ?? "").Contains(fillter));
                break;
case "orderId":
                        query = query.Where(c => c.row.OrderId.ToString().Trim().Contains(fillter));
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
                
                                if (parameters.TransactionTypeIds.Count > 0)
                                {
                                    query = query.Where(c => parameters.TransactionTypeIds.Contains(c.row.TransactionType.Id));
                                }
                                

                                if (parameters.TransactionStatusIds.Count > 0)
                                {
                                    query = query.Where(c => parameters.TransactionStatusIds.Contains(c.row.TransactionStatus.Id));
                                }
                                

                //3.Query second
                var query2 = query.Select(c => new TransactionsViewModel()
                {
                    Id = c.row.Id,
TransactionTypeId = c.tt.Id,
TransactionTypeName = c.tt.Name,
TransactionStatusId = c.ts.Id,
TransactionStatusName = c.ts.Name,
Active = c.row.Active,
Name = c.row.Name,
Description = c.row.Description,
Info = c.row.Info,
Amount = c.row.Amount,
SenderInfo = c.row.SenderInfo,
ReceiveInfo = c.row.ReceiveInfo,
OrderId = c.row.OrderId,
CreatedTime = c.row.CreatedTime,

                });
                //4. Sort
                query2 = query2.OrderByDynamic<TransactionsViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
                recordFiltered = await query2.CountAsync();
                //5. Return data
                return new DTResult<TransactionsViewModel>()
                {
                    data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                        draw = parameters.Draw,
                        recordsFiltered = recordFiltered,
                        recordsTotal = recordTotal
                };
            }
        }
    }


