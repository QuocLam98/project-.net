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
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Protocol.Plugins;
using Message = HomeDoctorSolution.Models.Message;

namespace HomeDoctorSolution.Repository
{
    public class MessageRepository : IMessageRepository
    {
        HomeDoctorContext db;

        public MessageRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Message>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Messages
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Message>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Messages
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Message>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Messages
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }


        public async Task<Message> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Messages.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }

            return null;
        }


        public async Task<Message> Add(Message obj)
        {
            if (db != null)
            {
                await db.Messages.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }

            return null;
        }


        public async Task Update(Message obj)
        {
            if (db != null)
            {
                //Update that object
                db.Messages.Attach(obj);
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.MessageTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.MessageStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Text).IsModified = true;
                db.Entry(obj).Property(x => x.RoomId).IsModified = true;
                db.Entry(obj).Property(x => x.AccountId).IsModified = true;
                db.Entry(obj).Property(x => x.ReceiverId).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Message obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Messages.Attach(obj);
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
                var obj = await db.Messages.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Messages.Remove(obj);

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
                    from row in db.Messages
                    where row.Active == 1
                    select row
                ).Count();
            }

            return result;
        }

        public async Task<DTResult<MessageViewModel>> ListServerSide(MessageDTParameters parameters)
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
            var query = from row in db.Messages
                join mt in db.MessageTypes on row.MessageTypeId equals mt.Id
                join ms in db.MessageStatuses on row.MessageStatusId equals ms.Id
                join r in db.Rooms on row.RoomId equals r.Id
                join a in db.Accounts on row.AccountId equals a.Id
                where row.Active == 1
                      && mt.Active == 1
                      && ms.Active == 1
                      && r.Active == 1
                      && a.Active == 1
                select new
                {
                    row,
                    mt,
                    ms,
                    r,
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
                    EF.Functions.Collate(c.row.Text.ToLower(), SQLParams.Latin_General)
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
                        case "text":
                            query = query.Where(c => (c.row.Text ?? "").Contains(fillter));
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

            if (parameters.MessageTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.MessageTypeIds.Contains(c.row.MessageType.Id));
            }


            if (parameters.MessageStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.MessageStatusIds.Contains(c.row.MessageStatus.Id));
            }


            if (parameters.RoomIds.Count > 0)
            {
                query = query.Where(c => parameters.RoomIds.Contains(c.row.Room.Id));
            }


            if (parameters.AccountIds.Count > 0)
            {
                query = query.Where(c => parameters.AccountIds.Contains(c.row.Account.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new MessageViewModel()
            {
                Id = c.row.Id,
                Active = c.row.Active,
                MessageTypeId = c.mt.Id,
                MessageTypeName = c.mt.Name,
                MessageStatusId = c.ms.Id,
                MessageStatusName = c.ms.Name,
                Name = c.row.Name,
                Description = c.row.Description,
                Text = c.row.Text,
                RoomId = c.r.Id,
                RoomName = c.r.Name,
                AccountId = c.a.Id,
                AccountName = c.a.Name,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<MessageViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<MessageViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }


        public async Task<Message> CheckExist(int? senderId, int? receiveId)
        {
            var data = await db.Messages.Where(c =>
                (c.AccountId == senderId || c.AccountId == receiveId) &&
                (c.ReceiverId == receiveId || c.ReceiverId == senderId) && c.Active == 1).FirstOrDefaultAsync();
            return data;
        }

        public DatabaseFacade GetDatabase()
        {
            return db.Database;
        }

        public async Task<List<MessageViewModel>> ListContact(int accountId, int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                var query = (from m in db.Messages
                    join a in db.Accounts on (m.AccountId == accountId ? m.AccountId : m.ReceiverId) equals a.Id
                    join r in db.Rooms on m.RoomId equals r.Id
                    where (a.Id == accountId && m.Active == 1 && a.Active == 1 && r.Active == 1)
                    orderby m.Id descending
                    select new MessageViewModel
                    {
                        Id = m.Id,
                        Text = m.Text,
                        RoomName = r.Name,
                        RoomId = m.RoomId,
                        AccountName = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.Name).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.Name).FirstOrDefault()),
                        AccountPhoto = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.Photo).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.Photo).FirstOrDefault()),
                        AccountId = m.AccountId,
                        ReceiverId = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.Id).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.Id).FirstOrDefault()),
                        MessageStatusId = m.MessageStatusId,
                        MessageTypeId = m.MessageTypeId,
                        CountTotalUnread = db.Messages.Where(c =>
                            c.ReceiverId == accountId && c.RoomId == m.RoomId && c.Active == 1 &&
                            c.MessageStatusId == 1000001).Count(),
                        CreatedTime = m.CreatedTime,
                        RoleId = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.RoleId).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.RoleId).FirstOrDefault()),
                    });
                var data = await query.GroupBy(c => c.RoomId)
                    .Select(x => x.OrderByDescending(c => c.Id).FirstOrDefault()).Skip(offSet).Take(pageSize)
                    .ToListAsync();
                return data.OrderByDescending(c => c.CreatedTime).ToList();
            }

            return null;
        }

        public async Task<List<MessageViewModel>> LoadUnread(int accountId, int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                var query = (from m in db.Messages
                    join a in db.Accounts on (m.AccountId == accountId ? m.AccountId : m.ReceiverId) equals a.Id
                    join r in db.Rooms on m.RoomId equals r.Id
                    where (a.Id == accountId && m.Active == 1 && a.Active == 1 && r.Active == 1)
                    orderby m.Id descending
                    select new MessageViewModel
                    {
                        Id = m.Id,
                        Text = m.Text,
                        RoomName = r.Name,
                        RoomId = m.RoomId,
                        AccountName = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.Name).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.Name).FirstOrDefault()),
                        AccountPhoto = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.Photo).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.Photo).FirstOrDefault()),
                        AccountId = m.AccountId,
                        ReceiverId = m.AccountId != accountId
                            ? (db.Accounts.Where(c => c.Id == m.AccountId).Select(c => c.Id).FirstOrDefault())
                            : (db.Accounts.Where(c => c.Id == m.ReceiverId).Select(c => c.Id).FirstOrDefault()),
                        MessageStatusId = m.MessageStatusId,
                        MessageTypeId = m.MessageTypeId,
                        CountTotalUnread = db.Messages.Where(c =>
                            c.ReceiverId == accountId && c.RoomId == m.RoomId && c.Active == 1 &&
                            c.MessageStatusId == 1000001).Count(),
                        CreatedTime = m.CreatedTime
                    });
                var data = await query.GroupBy(c => c.RoomId)
                    .Select(x => x.OrderByDescending(c => c.Id).FirstOrDefault()).Skip(offSet).Take(pageSize)
                    .ToListAsync();
                return data;
            }

            return null;
        }

        public async Task<List<MessageViewModel>> ListMessage(int accountId, int pageIndex, int pageSize, string roomName)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                var data = (from m in db.Messages
                    join a in db.Accounts on (m.AccountId == accountId ? m.AccountId : m.ReceiverId) equals a.Id
                    join r in db.Rooms on m.RoomId equals r.Id
                    where (a.Id == accountId && m.Active == 1 && a.Active == 1 && r.Active == 1 && r.Name == roomName)
                    orderby m.Id descending
                    select new MessageViewModel
                    {
                        Id = m.Id,
                        RoomId = m.RoomId,
                        Text = m.Text,
                        RoomName = r.Name,
                        AccountName = a.Name,
                        AccountId = m.AccountId,
                        ReceiverId = m.ReceiverId,
                        MessageStatusId = m.MessageStatusId,
                        MessageTypeId = m.MessageTypeId,
                        CreatedTime = m.CreatedTime
                    });

                if (pageSize == -1)
                {
                    return await data.Where(c => c.ReceiverId != accountId).ToListAsync();
                }
                else
                {
                    return await data.Skip(offSet).Take(pageSize).ToListAsync();
                }
            }

            return null;
        }

        public async Task UpdateMany(List<Message> messages)
        {
            if (db != null)
            {
                foreach (var obj in messages)
                {
                    obj.MessageStatusId = 1000002;
                    //Update that object
                    db.Messages.Attach(obj);
                    db.Entry(obj).Property(x => x.MessageStatusId).IsModified = true;
                }

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}