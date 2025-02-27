
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
    public class SurveyRepository : ISurveyRepository
    {
        HomeDoctorContext db;
        public SurveyRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Survey>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Surveys
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Survey>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Surveys
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }
            return null;
        }


        public async Task<List<Survey>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            if (db != null)
            {
                return await (
                    from row in db.Surveys
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }
            return null;
        }


        public async Task<Survey> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Surveys.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }
            return null;
        }


        public async Task<Survey> Add(Survey obj)
        {
            if (db != null)
            {
                await db.Surveys.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }
            return null;
        }


        public async Task Update(Survey obj)
        {
            if (db != null)
            {
                //Update that object
                db.Surveys.Attach(obj);
                db.Entry(obj).Property(x => x.SurveyTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.SurveyStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Url).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.Video).IsModified = true;
                db.Entry(obj).Property(x => x.ViewCount).IsModified = true;
                db.Entry(obj).Property(x => x.CommentCount).IsModified = true;
                db.Entry(obj).Property(x => x.LikeCount).IsModified = true;
                db.Entry(obj).Property(x => x.Score).IsModified = true;
                db.Entry(obj).Property(x => x.Text).IsModified = true;
                db.Entry(obj).Property(x => x.PublishedTime).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Survey obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Surveys.Attach(obj);
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
                var obj = await db.Surveys.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Surveys.Remove(obj);

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
                    from row in db.Surveys
                    where row.Active == 1
                    select row
                            ).Count();
            }

            return result;
        }
        public async Task<DTResult<SurveyViewModel>> ListServerSide(SurveyDTParameters parameters)
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
            var query = from row in db.Surveys

                        join st in db.SurveyTypes on row.SurveyTypeId equals st.Id
                        join ss in db.SurveyStatuses on row.SurveyStatusId equals ss.Id

                        where row.Active == 1
                                        && st.Active == 1
    && ss.Active == 1

                        select new
                        {
                            row,
                            st,
                            ss
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
EF.Functions.Collate(c.row.Url.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Video.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.ViewCount.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.CommentCount.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.LikeCount.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Score.ToString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.Text.ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
EF.Functions.Collate(c.row.PublishedTime.ToCustomString().ToLower(), SQLParams.Latin_General).Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
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
                      
                        case "url":
                            query = query.Where(c => (c.row.Url ?? "").Contains(fillter));
                            break;
                        case "photo":
                            query = query.Where(c => (c.row.Photo ?? "").Contains(fillter));
                            break;
                        case "video":
                            query = query.Where(c => (c.row.Video ?? "").Contains(fillter));
                            break;
                        case "viewCount":
                            query = query.Where(c => c.row.ViewCount.ToString().Trim().Contains(fillter));
                            break;
                        case "commentCount":
                            query = query.Where(c => c.row.CommentCount.ToString().Trim().Contains(fillter));
                            break;
                        case "likeCount":
                            query = query.Where(c => c.row.LikeCount.ToString().Trim().Contains(fillter));
                            break;
                        case "score":
                            query = query.Where(c => c.row.Score.ToString().Trim().Contains(fillter));
                            break;
                        case "text":
                            query = query.Where(c => (c.row.Text ?? "").Contains(fillter));
                            break;
                        case "publishedTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
                                query = query.Where(c => c.row.PublishedTime >= startDate && c.row.PublishedTime <= endDate);
                            }
                            else
                            {
                                var date = DateTime.ParseExact(fillter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                query = query.Where(c => c.row.PublishedTime.Date == date.Date);
                            }
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

            if (parameters.SurveyTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.SurveyTypeIds.Contains(c.row.SurveyType.Id));
            }


            if (parameters.SurveyStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.SurveyStatusIds.Contains(c.row.SurveyStatus.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new SurveyViewModel()
            {
                Id = c.row.Id,
                SurveyTypeId = c.st.Id,
                SurveyTypeName = c.st.Name,
                SurveyStatusId = c.ss.Id,
                SurveyStatusName = c.ss.Name,
                Active = c.row.Active,
                Name = c.row.Name,
                Description = c.row.Description,
                Url = c.row.Url,
                Photo = c.row.Photo,
                Video = c.row.Video,
                ViewCount = c.row.ViewCount,
                CommentCount = c.row.CommentCount,
                LikeCount = c.row.LikeCount,
                Score = c.row.Score,
                Text = c.row.Text,
                PublishedTime = c.row.PublishedTime,
                CreatedTime = c.row.CreatedTime,

            });
            //4. Sort
            query2 = query2.OrderByDynamic<SurveyViewModel>(orderCritirea, orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<SurveyViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }
    }
}


