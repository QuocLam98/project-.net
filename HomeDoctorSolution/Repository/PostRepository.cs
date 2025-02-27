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
using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Util.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HomeDoctor.Util.DTParameters;
using HomeDoctorSolution.Constants;
using HomeDoctor.Models.ViewModels.Post;

namespace HomeDoctorSolution.Repository
{
    public class PostRepository : IPostRepository
    {
        HomeDoctorContext db;

        public PostRepository(HomeDoctorContext _db)
        {
            db = _db;
        }


        public async Task<List<Post>> List()
        {
            if (db != null)
            {
                return await (
                    from row in db.Posts
                    where (row.Active == 1)
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<Post>> Search(string keyword)
        {
            if (db != null)
            {
                return await (
                    from row in db.Posts
                    where (row.Active == 1 && (row.Name.Contains(keyword) || row.Description.Contains(keyword)))
                    orderby row.Id descending
                    select row
                ).ToListAsync();
            }

            return null;
        }


        public async Task<List<PostViewModel>> ListPaging(int pageIndex, int pageSize)
        {
            int offSet = (pageIndex - 1) * pageSize;

            if (db != null)
            {
                return await (
                    from row in db.Posts
                    join pt in db.PostTypes on row.PostTypeId equals pt.Id
                    join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                    join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                    join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                    join a in db.Authors on row.AuthorId equals a.Id
                    where row.Active == 1 && pt.Active == 1 && pc.Active == 1 && pl.Active == 1 && pps.Active == 1 &&
                          a.Active == 1
                    orderby row.CreatedTime descending
                    select new PostViewModel
                    {
                        Id = row.Id,
                        PostTypeId = pt.Id,
                        PostTypeName = pt.Name,
                        PostAccountId = row.PostAccountId,
                        PostCategoryId = pc.Id,
                        PostCategoryName = pc.Name,
                        PostLayoutId = pl.Id,
                        PostLayoutName = pl.Name,
                        PostPublishStatusId = pps.Id,
                        PostPublishStatusName = pps.Name,
                        AuthorId = a.Id,
                        AuthorName = a.Name,
                        GuId = row.GuId,
                        Active = row.Active,
                        Photo = row.Photo,
                        Video = row.Video,
                        ViewCount = row.ViewCount,
                        CommentCount = row.CommentCount,
                        LikeCount = row.LikeCount,
                        Url = row.Url,
                        Name = row.Name,
                        Description = row.Description,
                        // Text = row.Text,
                        DownloadLink = row.DownloadLink,
                        PublishedTime = row.PublishedTime,
                        CreatedTime = row.CreatedTime,
                    }
                ).Skip(offSet).Take(pageSize).ToListAsync();
            }

            return null;
        }



        public async Task<Post> Detail(int? id)
        {
            if (db != null)
            {
                return await db.Posts.FirstOrDefaultAsync(row => row.Active == 1 && row.Id == id);
            }

            return null;
        }


        public async Task<Post> Add(Post obj)
        {
            if (db != null)
            {
                await db.Posts.AddAsync(obj);
                await db.SaveChangesAsync();
                return obj;
            }

            return null;
        }


        public async Task Update(Post obj)
        {
            if (db != null)
            {
                //Update that object
                db.Posts.Attach(obj);
                db.Entry(obj).Property(x => x.PostTypeId).IsModified = true;
                db.Entry(obj).Property(x => x.PostAccountId).IsModified = true;
                db.Entry(obj).Property(x => x.PostCategoryId).IsModified = true;
                db.Entry(obj).Property(x => x.PostLayoutId).IsModified = true;
                db.Entry(obj).Property(x => x.PostPublishStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.AuthorId).IsModified = true;
                db.Entry(obj).Property(x => x.GuId).IsModified = true;
                db.Entry(obj).Property(x => x.Active).IsModified = true;
                db.Entry(obj).Property(x => x.Photo).IsModified = true;
                db.Entry(obj).Property(x => x.Video).IsModified = true;
                db.Entry(obj).Property(x => x.ViewCount).IsModified = true;
                db.Entry(obj).Property(x => x.CommentCount).IsModified = true;
                db.Entry(obj).Property(x => x.LikeCount).IsModified = true;
                db.Entry(obj).Property(x => x.Url).IsModified = true;
                db.Entry(obj).Property(x => x.Name).IsModified = true;
                db.Entry(obj).Property(x => x.Description).IsModified = true;
                db.Entry(obj).Property(x => x.Text).IsModified = true;
                db.Entry(obj).Property(x => x.DownloadLink).IsModified = true;
                db.Entry(obj).Property(x => x.PublishedTime).IsModified = true;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }


        public async Task Delete(Post obj)
        {
            if (db != null)
            {
                //Update that obj
                db.Posts.Attach(obj);
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
                var obj = await db.Posts.FirstOrDefaultAsync(x => x.Id == objId);

                if (obj != null)
                {
                    //Delete that obj
                    db.Posts.Remove(obj);

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
                    from row in db.Posts
                    where row.Active == 1
                    select row
                ).Count();
            }

            return result;
        }

        public async Task<DTResult<PostViewModel>> ListServerSide(PostDTParameters parameters)
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
            var query = from row in db.Posts
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                join a in db.Authors on row.AuthorId equals a.Id
                where row.Active == 1
                      && pt.Active == 1
                      && pc.Active == 1
                      && pl.Active == 1
                      && pps.Active == 1
                      && a.Active == 1
                select new
                {
                    row,
                    pt,
                    pc,
                    pl,
                    pps,
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
                    EF.Functions.Collate(c.row.PostAccountId.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.GuId.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Video.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ViewCount.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.CommentCount.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.LikeCount.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Url.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Text.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.DownloadLink.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.PublishedTime.ToCustomString().ToLower(), SQLParams.Latin_General)
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
                        case "postAccountId":
                            query = query.Where(c => c.row.PostAccountId.ToString().Trim().Contains(fillter));
                            break;
                        case "guId":
                            query = query.Where(c => (c.row.GuId ?? "").Contains(fillter));
                            break;
                        case "active":
                            query = query.Where(c => c.row.Active.ToString().Trim().Contains(fillter));
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
                        case "url":
                            query = query.Where(c => (c.row.Url ?? "").Contains(fillter));
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
                        case "downloadLink":
                            query = query.Where(c => (c.row.DownloadLink ?? "").Contains(fillter));
                            break;
                        case "publishedTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                    .AddDays(1).AddSeconds(-1);
                                query = query.Where(c =>
                                    c.row.PublishedTime >= startDate && c.row.PublishedTime <= endDate);
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

            if (parameters.PostTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.PostTypeIds.Contains(c.row.PostType.Id));
            }


            if (parameters.PostCategoryIds.Count > 0)
            {
                query = query.Where(c => parameters.PostCategoryIds.Contains(c.row.PostCategory.Id));
            }


            if (parameters.PostLayoutIds.Count > 0)
            {
                query = query.Where(c => parameters.PostLayoutIds.Contains(c.row.PostLayout.Id));
            }


            if (parameters.PostPublishStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.PostPublishStatusIds.Contains(c.row.PostPublishStatus.Id));
            }


            if (parameters.AuthorIds.Count > 0)
            {
                query = query.Where(c => parameters.AuthorIds.Contains(c.row.Author.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new PostViewModel()
            {
                Id = c.row.Id,
                PostTypeId = c.pt.Id,
                PostTypeName = c.pt.Name,
                PostAccountId = c.row.PostAccountId,
                PostCategoryId = c.pc.Id,
                PostCategoryName = c.pc.Name,
                PostLayoutId = c.pl.Id,
                PostLayoutName = c.pl.Name,
                PostPublishStatusId = c.pps.Id,
                PostPublishStatusName = c.pps.Name,
                AuthorId = c.a.Id,
                AuthorName = c.a.Name,
                GuId = c.row.GuId,
                Active = c.row.Active,
                Photo = c.row.Photo,
                Video = c.row.Video,
                ViewCount = c.row.ViewCount,
                CommentCount = c.row.CommentCount,
                LikeCount = c.row.LikeCount,
                Url = c.row.Url,
                Name = c.row.Name,
                Description = c.row.Description,
                Text = c.row.Text,
                DownloadLink = c.row.DownloadLink,
                PublishedTime = c.row.PublishedTime,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<PostViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<PostViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<List<Post>> PostChartReportByTime(string startDate, string endDate)
        {
            var dateStart = DateTime.ParseExact(startDate.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dateEnd = DateTime.ParseExact(endDate.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1)
                .AddSeconds(-1);
            var query = (
                from row in db.Posts
                where (row.Active == 1 && row.CreatedTime >= dateStart && row.CreatedTime <= dateEnd)
                orderby row.CreatedTime ascending
                select row
            );
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<PostReport>> PostQuantityReport()
        {
            var report = new PostReport();
            var list = new List<PostReport>();
            report.postTotal = await db.Posts.Where(p => p.Active == 1).CountAsync();
            list.Add(report);
            return list;
        }

        public async Task<PagingData<List<PostViewModel>>> ListPost(PostParameters parameter)
        {
            //Khai báo biến
            var pageIndex = parameter.PageIndex;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;

            var result = await (from row in db.Posts
                join at in db.Authors on row.AuthorId equals at.Id
                join a in db.Accounts on at.AccountId equals a.Id
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                join ppst in db.PostPublishStatuses on row.PostPublishStatusId equals ppst.Id
                where row.Active == 1
                      && at.Active == 1
                      && a.Active == 1
                      && pc.Active == 1
                      && pt.Active == 1
                      && pl.Active == 1
                      && ppst.Active == 1
                      && row.PostPublishStatusId == SystemConstant.POST_PUBLISH_STATUS_PUBLIC
                      && row.PostTypeId == PostContants.POST_TYPE_ID
                orderby row.CreatedTime descending
                select new PostViewModel
                {
                    Active = row.Active,
                    PostLayoutId = row.PostLayoutId,
                    Id = row.Id,
                    PostCategoryId = row.PostCategoryId,
                    PostTypeId = row.PostTypeId,
                    AuthorId = row.AuthorId,
                    AuthorName = at.Name,
                    AuthorImage = a.Photo,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    Name = row.Name,
                    Photo = row.Photo,
                    PostCategoryName = pc.Name,
                    PostTypeName = pt.Name,
                    PublishedTime = row.PublishedTime,
                    Text = row.Text,
                    AuthorUserName = a.Username
                }).ToListAsync();

            var allRecord = result.Count();

            var recordsTotal = result.Count;
            return new PagingData<List<PostViewModel>>
            {
                DataSource = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }

        public async Task<object> LoadDataFilterPostHomePageAsync()
        {
            //Lấy data loại bài viết
            var postCategoryData = await (from pc in db.PostCategories
                from p in db.Posts
                where pc.Active == 1
                      && p.Active == 1
                      && pc.ParentId == SystemConstant.CATEGORY_NEWS_ID
                select new FilterByPostCategoryViewModel
                {
                    Id = pc.Id,
                    Name = pc.Name,
                    CountPost = (from row in db.Posts
                        from row1 in db.PostCategories
                        where row1.Id == row.PostCategoryId
                              && row.Active == 1
                              && row1.Active == 1
                              && row.PostCategoryId == pc.Id
                              && row.PostTypeId == SystemConstant.POST_TYPE_TEXT
                        select row).Count()
                }).Distinct().ToListAsync();
            //Lấy data thẻ bài viết
            var postTagsData = await (from t in db.Tags
                from p in db.Posts
                where t.Active == 1
                      && p.Active == 1
                select new TagViewModel
                {
                    Id = t.Id,
                    TagName = t.Name,
                }).Distinct().ToListAsync();

            return new
            {
                PostCategoryData = postCategoryData,
                PostTagsData = postTagsData
            };
        }

        public async Task<bool> IsNameExistInSameCategoryAsync(int id, int postCategoryId, string name)
        {
            return await db.Posts.AnyAsync(c =>
                c.Name.ToLower().Trim() == name.ToLower().Trim() && c.PostCategoryId == postCategoryId && c.Id != id &&
                c.Active == 1);
        }

        public async Task<PostViewModel> DetailPostByIdAsync(int id)
        {
            return await (from p in db.Posts
                join pc in db.PostCategories on p.PostCategoryId equals pc.Id
                join pt in db.PostTypes on p.PostTypeId equals pt.Id
                join at in db.Authors on p.AuthorId equals at.Id
                join a in db.Accounts on at.AccountId equals a.Id
                join pl in db.PostLayouts on p.PostLayoutId equals pl.Id
                where p.Id == id
                      && p.Active == 1
                      && pc.Active == 1
                      && pt.Active == 1
                      && a.Active == 1
                      && pl.Active == 1
                      && at.Active == 1
                let tags = (from ptag in db.PostTags
                    join tag in db.Tags on ptag.TagId equals tag.Id
                    where tag.Active == 1
                          && ptag.PostId == p.Id
                          && ptag.Active == 1
                    select new TagViewModel
                    {
                        Id = tag.Id,
                        CreatedTime = tag.CreatedTime,
                        Name = tag.Name,
                    }).ToList()
                select new PostViewModel
                {
                    Id = p.Id,
                    Active = p.Active,
                    AuthorId = p.AuthorId,
                    CreatedTime = p.CreatedTime,
                    Name = p.Name,
                    Photo = p.Photo,
                    PostCategoryId = p.PostCategoryId,
                    PostCategoryName = pc.Name,
                    PostLayoutId = p.PostLayoutId,
                    PostLayoutName = pl.Name,
                    PostTypeId = p.PostTypeId,
                    PostTypeName = pt.Name,
                    Text = p.Text,
                    AuthorName = at.Name,
                    AuthorImage = a.Photo,
                    PublishedTime = p.PublishedTime,
                    ListTag = tags,
                    DownloadLink = p.DownloadLink,
                    Description = p.Description,
                    AuthorUserName = a.Username
                }).FirstOrDefaultAsync();
        }

        public async Task<PostViewModel> LatestPostsByTime()
        {
            if (db != null)
            {
                var query = from row in db.Posts
                    join pt in db.PostTypes on row.PostTypeId equals pt.Id
                    join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                    join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                    join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                    join a in db.Authors on row.AuthorId equals a.Id
                    join ac in db.Accounts on a.AccountId equals ac.Id
                    where row.Active == 1
                          && pt.Active == 1
                          && pc.Active == 1
                          && pl.Active == 1
                          && pps.Active == 1
                          && a.Active == 1
                          && row.PostTypeId == PostContants.POST_TYPE_ID
                    orderby row.PublishedTime descending
                    select new PostViewModel
                    {
                        Photo = row.Photo,
                        Id = row.Id,
                        Active = row.Active,
                        AuthorName = a.Name,
                        AuthorImage = ac.Photo,
                        Description = row.Description,
                        Name = row.Name,
                        PublishedTime = row.PublishedTime,
                        Text = row.Text,
                        AuthorUserName = ac.Username
                    };
                return await query.FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<PostViewModel>> Top3Post()
        {
            if (db != null)
            {
                var query = from row in db.Posts
                    join pt in db.PostTypes on row.PostTypeId equals pt.Id
                    join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                    join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                    join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                    join a in db.Authors on row.AuthorId equals a.Id
                    join ac in db.Accounts on a.AccountId equals ac.Id
                    where row.Active == 1
                          && pt.Active == 1
                          && pc.Active == 1
                          && pl.Active == 1
                          && pps.Active == 1
                          && a.Active == 1
                          && row.PostTypeId == PostContants.POST_TYPE_ID
                    orderby row.PublishedTime descending
                    select new PostViewModel
                    {
                        Photo = row.Photo,
                        Id = row.Id,
                        Active = row.Active,
                        AuthorName = a.Name,
                        AuthorImage = ac.Photo,
                        Description = row.Description,
                        Name = row.Name,
                        PublishedTime = row.PublishedTime,
                        Text = row.Text,
                        Username = ac.Username,
                    };
                return await query.Take(3).ToListAsync();
            }

            return null;
        }

        public DatabaseFacade GetDatabase()
        {
            return db.Database;
        }

        public async Task<List<PostViewModel>> ListPostMobile(int pageIndex, int pageSize)
        {
            int offSet = 0;
            offSet = (pageIndex - 1) * pageSize;
            var result = await (from row in db.Posts
                join at in db.Authors on row.AuthorId equals at.Id
                join a in db.Accounts on at.AccountId equals a.Id
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                where row.Active == 1
                      && a.Active == 1
                      && pc.Active == 1
                      && pt.Active == 1
                      && pl.Active == 1
                orderby row.CreatedTime
                select new PostViewModel
                {
                    Active = row.Active,
                    PostLayoutId = row.PostLayoutId,
                    Id = row.Id,
                    PostCategoryId = row.PostCategoryId,
                    PostTypeId = row.PostTypeId,
                    AuthorId = row.AuthorId,
                    AuthorName = at.Name,
                    AuthorImage = a.Photo,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    Name = row.Name,
                    Photo = row.Photo,
                    PostCategoryName = pc.Name,
                    PostTypeName = pt.Name,
                    PublishedTime = row.PublishedTime,
                    Text = row.Text,
                    ListTag = (from tags in db.Tags
                        from postTag in db.PostTags
                        where tags.Active == 1
                              && postTag.Active == 1 && postTag.PostId == row.Id && postTag.TagId == tags.Id
                        select new TagViewModel
                        {
                            Id = tags.Id,
                            Name = tags.Name,
                        }).ToList(),
                }).Skip(offSet).Take(pageSize).ToListAsync();
            return result;
        }

        public async Task<DTResult<PostViewModel>> ListLibraryServerSide(PostDTParameters parameters)
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
            var query = from row in db.Posts
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                join a in db.Authors on row.AuthorId equals a.Id
                where row.Active == 1
                      && pt.Active == 1
                      && pc.Active == 1
                      && pl.Active == 1
                      && pps.Active == 1
                      && a.Active == 1
                      && row.PostTypeId == SystemConstant.POST_TYPE_NEW
                select new
                {
                    row,
                    pt,
                    pc,
                    pl,
                    pps,
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
                    EF.Functions.Collate(c.row.PostAccountId.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.GuId.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Active.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Photo.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Video.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.ViewCount.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.CommentCount.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.LikeCount.ToString().ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Url.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Name.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Description.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.Text.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.DownloadLink.ToLower(), SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(searchAll, SQLParams.Latin_General)) ||
                    EF.Functions.Collate(c.row.PublishedTime.ToCustomString().ToLower(), SQLParams.Latin_General)
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
                        case "postAccountId":
                            query = query.Where(c => c.row.PostAccountId.ToString().Trim().Contains(fillter));
                            break;
                        case "guId":
                            query = query.Where(c => (c.row.GuId ?? "").Contains(fillter));
                            break;
                        case "active":
                            query = query.Where(c => c.row.Active.ToString().Trim().Contains(fillter));
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
                        case "url":
                            query = query.Where(c => (c.row.Url ?? "").Contains(fillter));
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
                        case "downloadLink":
                            query = query.Where(c => (c.row.DownloadLink ?? "").Contains(fillter));
                            break;
                        case "publishedTime":
                            if (fillter.Contains(" - "))
                            {
                                var dates = fillter.Split(" - ");
                                var startDate = DateTime.ParseExact(dates[0], "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture);
                                var endDate = DateTime.ParseExact(dates[1], "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                    .AddDays(1).AddSeconds(-1);
                                query = query.Where(c =>
                                    c.row.PublishedTime >= startDate && c.row.PublishedTime <= endDate);
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

            if (parameters.PostTypeIds.Count > 0)
            {
                query = query.Where(c => parameters.PostTypeIds.Contains(c.row.PostType.Id));
            }


            if (parameters.PostCategoryIds.Count > 0)
            {
                query = query.Where(c => parameters.PostCategoryIds.Contains(c.row.PostCategory.Id));
            }


            if (parameters.PostLayoutIds.Count > 0)
            {
                query = query.Where(c => parameters.PostLayoutIds.Contains(c.row.PostLayout.Id));
            }


            if (parameters.PostPublishStatusIds.Count > 0)
            {
                query = query.Where(c => parameters.PostPublishStatusIds.Contains(c.row.PostPublishStatus.Id));
            }


            if (parameters.AuthorIds.Count > 0)
            {
                query = query.Where(c => parameters.AuthorIds.Contains(c.row.Author.Id));
            }


            //3.Query second
            var query2 = query.Select(c => new PostViewModel()
            {
                Id = c.row.Id,
                PostTypeId = c.pt.Id,
                PostTypeName = c.pt.Name,
                PostAccountId = c.row.PostAccountId,
                PostCategoryId = c.pc.Id,
                PostCategoryName = c.pc.Name,
                PostLayoutId = c.pl.Id,
                PostLayoutName = c.pl.Name,
                PostPublishStatusId = c.pps.Id,
                PostPublishStatusName = c.pps.Name,
                AuthorId = c.a.Id,
                AuthorName = c.a.Name,
                GuId = c.row.GuId,
                Active = c.row.Active,
                Photo = c.row.Photo,
                Video = c.row.Video,
                ViewCount = c.row.ViewCount,
                CommentCount = c.row.CommentCount,
                LikeCount = c.row.LikeCount,
                Url = c.row.Url,
                Name = c.row.Name,
                Description = c.row.Description,
                Text = c.row.Text,
                DownloadLink = c.row.DownloadLink,
                PublishedTime = c.row.PublishedTime,
                CreatedTime = c.row.CreatedTime,
            });
            //4. Sort
            query2 = query2.OrderByDynamic<PostViewModel>(orderCritirea,
                orderDirectionASC ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            recordFiltered = await query2.CountAsync();
            //5. Return data
            return new DTResult<PostViewModel>()
            {
                data = await query2.Skip(parameters.Start).Take(parameters.Length).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordTotal
            };
        }

        public async Task<bool> CheckNameIsActive(string name, int id)
        {
            return await db.Posts.AnyAsync(e => e.Name == name && e.Active == 1 && e.Id != id);
            //true trùng 
            //fales chưa
        }

        public async Task<PagingData<List<PostViewModel>>> ListPostByPostCategory(PostParameters parameter)
        {
            var pageIndex = parameter.PageIndex;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;

            var result = await (from row in db.Posts
                join at in db.Authors on row.AuthorId equals at.Id
                join a in db.Accounts on at.AccountId equals a.Id
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                where row.Active == 1
                      && row.PostCategoryId == parameter.PostCategoryId
                      && row.PostTypeId == parameter.PostTypeId
                      && a.Active == 1
                      && pc.Active == 1
                      && pt.Active == 1
                orderby row.CreatedTime
                select new PostViewModel
                {
                    Active = row.Active,
                    Id = row.Id,
                    PostCategoryId = row.PostCategoryId,
                    PostTypeId = row.PostTypeId,
                    AuthorId = row.AuthorId,
                    AuthorName = at.Name,
                    AuthorImage = a.Photo,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    Name = row.Name,
                    Photo = row.Photo,
                    PostCategoryName = pc.Name,
                    PostTypeName = pt.Name,
                    PublishedTime = row.PublishedTime,
                    Text = row.Text,
                    AuthorUserName = a.Username
                }).ToListAsync();

            var allRecord = result.Count();

            var recordsTotal = result.Count;
            return new PagingData<List<PostViewModel>>
            {
                DataSource = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }

        public async Task<PagingData<List<PostViewModel>>> ListPostByPostTag(PostParameters parameter)
        {
            var pageIndex = parameter.PageIndex;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;

            var result = await (from row in db.Posts
                join at in db.Authors on row.AuthorId equals at.Id
                join a in db.Accounts on at.AccountId equals a.Id
                join ptag in db.PostTags on row.Id equals ptag.PostId
                join t in db.Tags on ptag.TagId equals t.Id
                where row.Active == 1
                      && row.PostTypeId == parameter.PostTypeId
                      && ptag.TagId == parameter.PostTagId
                      && ptag.Active == 1
                      && t.Active == 1
                orderby row.CreatedTime
                select new PostViewModel
                {
                    Active = row.Active,
                    Id = row.Id,
                    PostCategoryId = row.PostCategoryId,
                    PostTypeId = row.PostTypeId,
                    AuthorId = row.AuthorId,
                    AuthorName = at.Name,
                    AuthorImage = a.Photo,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    Name = row.Name,
                    Photo = row.Photo,
                    PublishedTime = row.PublishedTime,
                    Text = row.Text,
                    TagId = t.Id,
                    TagName = t.Name,
                    AuthorUserName = a.Username
                }).ToListAsync();

            var allRecord = result.Count();

            var recordsTotal = result.Count;
            return new PagingData<List<PostViewModel>>
            {
                DataSource = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }

        public async Task<PagingData<List<PostViewModel>>> ListPostByPostName(PostParameters parameter)
        {
            string keyword = "";
            if (parameter.Search != null)
            {
                keyword = parameter.Search.Trim();
            }

            var pageIndex = parameter.PageIndex;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;

            var result = await (from row in db.Posts
                join at in db.Authors on row.AuthorId equals at.Id
                join a in db.Accounts on at.AccountId equals a.Id
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                where row.Active == 1
                      && (EF.Functions.Collate(row.Name, SQLParams.Latin_General)
                              .Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                          || EF.Functions.Collate(row.Description, SQLParams.Latin_General)
                              .Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                          || EF.Functions.Collate(row.Text, SQLParams.Latin_General)
                              .Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)))
                      && row.PostTypeId == parameter.PostTypeId
                      && pc.Active == 1
                      && pt.Active == 1
                orderby row.CreatedTime
                select new PostViewModel
                {
                    Active = row.Active,
                    PostLayoutId = row.PostLayoutId,
                    Id = row.Id,
                    PostTypeId = row.PostTypeId,
                    AuthorId = row.AuthorId,
                    AuthorName = at.Name,
                    AuthorImage = a.Photo,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    Name = row.Name,
                    Photo = row.Photo,
                    PostCategoryName = pc.Name,
                    PostTypeName = pt.Name,
                    PublishedTime = row.PublishedTime,
                    Text = row.Text,
                }).ToListAsync();

            var allRecord = result.Count();

            var recordsTotal = result.Count;
            return new PagingData<List<PostViewModel>>
            {
                DataSource = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }

        public async Task UpdateByRole(Post obj, int roleId)
        {
            if (db != null)
            {
                //Update that object
                db.Posts.Attach(obj);
                db.Entry(obj).Property(x => x.PostTypeId).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.PostAccountId).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.PostCategoryId).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.PostLayoutId).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.PostPublishStatusId).IsModified = true;
                db.Entry(obj).Property(x => x.AuthorId).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.GuId).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Active).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Photo).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Video).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.ViewCount).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.CommentCount).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.LikeCount).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Url).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Name).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Description).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.Text).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.DownloadLink).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;
                db.Entry(obj).Property(x => x.PublishedTime).IsModified =
                    roleId != SystemConstant.ROLE_TEENAGER_MOD ? true : false;

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<PostViewModel>> DetailById(int? id)
        {
            if (db != null)
            {
                return await (
                        from row in db.Posts
                        where (row.Active == 1 && row.Id == id)
                        select new PostViewModel
                        {
                            Active = row.Active,
                            AuthorId = row.AuthorId,
                            CommentCount = row.CommentCount,
                            CreatedTime = row.CreatedTime,
                            Description = row.Description,
                            DownloadLink = row.DownloadLink,
                            Id = row.Id,
                            LikeCount = row.LikeCount,
                            Name = row.Name,
                            Photo = row.Photo,
                            PostAccountId = row.PostAccountId,
                            PostCategoryId = row.PostCategoryId,
                            PostCommentStatusId = row.PostCommentStatusId,
                            PostPublishStatusId = row.PostPublishStatusId,
                            PostTypeId = row.PostTypeId,
                            Text = row.Text,
                            Url = row.Url,
                            Video = row.Video,
                            ViewCount = row.ViewCount,
                            PublishedTime = row.PublishedTime,
                            ListTag = db.PostTags.Where(p => p.PostId == row.Id && p.Active == 1).Select(x =>
                                    new TagViewModel
                                    {
                                        Id = x.TagId
                                    })
                                .ToList()
                        })
                    .ToListAsync();
            }

            return null;
        }

        public async Task<List<PostViewModel>> ListFourPostByTimeDesc()
        {
            if (db != null)
            {
                return await (
                    from row in db.Posts
                    join pt in db.PostTypes on row.PostTypeId equals pt.Id
                    join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                    join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                    join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                    join a in db.Authors on row.AuthorId equals a.Id
                    where row.Active == 1 && pt.Active == 1 && pc.Active == 1 && pl.Active == 1 && pps.Active == 1 &&
                          a.Active == 1
                    orderby row.CreatedTime descending
                    select new PostViewModel
                    {
                        Id = row.Id,
                        PostTypeId = pt.Id,
                        PostTypeName = pt.Name,
                        PostAccountId = row.PostAccountId,
                        PostCategoryId = pc.Id,
                        PostCategoryName = pc.Name,
                        PostLayoutId = pl.Id,
                        PostLayoutName = pl.Name,
                        PostPublishStatusId = pps.Id,
                        PostPublishStatusName = pps.Name,
                        AuthorId = a.Id,
                        AuthorName = a.Name,
                        GuId = row.GuId,
                        Active = row.Active,
                        Photo = row.Photo,
                        Video = row.Video,
                        ViewCount = row.ViewCount,
                        CommentCount = row.CommentCount,
                        LikeCount = row.LikeCount,
                        Url = row.Url,
                        Name = row.Name,
                        Description = row.Description,
                        // Text = row.Text,
                        DownloadLink = row.DownloadLink,
                        PublishedTime = row.PublishedTime,
                        CreatedTime = row.CreatedTime,
                    }
                ).Take(4).ToListAsync();
            }

            return null;
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<PagingData<List<PostViewModel>>> ListPostHomePageAsync(SearchingPostParameters parameters)
        {
            var keyword = parameters.Keywords;
            var pageIndex = parameters.PageIndex;
            var pageSize = parameters.PageSize;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            var categoryId = parameters.CategoryPostId;

            if (parameters.OrderCriteria != null)
            {
                orderCriteria = parameters.OrderCriteria;
                orderAscendingDirection = parameters.OrderAscendingDirection.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            // Lấy dữ liệu
            var result = (
                from row in db.Posts
                join pc in db.PostCategories on row.PostCategoryId equals pc.Id
                join pt in db.PostTypes on row.PostTypeId equals pt.Id
                join pl in db.PostLayouts on row.PostLayoutId equals pl.Id
                join pps in db.PostPublishStatuses on row.PostPublishStatusId equals pps.Id
                join a in db.Authors on row.AuthorId equals a.Id
                where row.Active == 1 && pc.Active == 1
                select new PostViewModel
                {
                    Id = row.Id,
                    PostTypeId = pt.Id,
                    PostTypeName = pt.Name,
                    PostAccountId = row.PostAccountId,
                    PostCategoryId = pc.Id,
                    PostCategoryName = pc.Name,
                    PostLayoutId = pl.Id,
                    PostLayoutName = pl.Name,
                    PostPublishStatusId = pps.Id,
                    PostPublishStatusName = pps.Name,
                    AuthorId = a.Id,
                    AuthorName = a.Name,
                    GuId = row.GuId,
                    Active = row.Active,
                    Photo = row.Photo,
                    Video = row.Video,
                    ViewCount = row.ViewCount,
                    CommentCount = row.CommentCount,
                    LikeCount = row.LikeCount,
                    Url = row.Url,
                    Name = row.Name,
                    Description = row.Description,
                    DownloadLink = row.DownloadLink,
                    PublishedTime = row.PublishedTime,
                    CreatedTime = row.CreatedTime,
                }
            );

            // Tìm kiếm tất cả
            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(s =>
                    EF.Functions.Collate(s.Name, SQLParams.Latin_General)
                        .Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)));
            }

            if (categoryId.Length != 0)
            {
                var categoryResult = categoryId.Split(",").Select(n => Int64.Parse(n));
                result = result.Where(r => categoryResult.Contains(r.PostCategoryId));
            }

            // Orderby
            if (orderAscendingDirection)
            {
                result = result.OrderBy(x => x.CreatedTime);
            }
            else
            {
                result = result.OrderByDescending(x => x.CreatedTime);
            }

            var recordsTotal = await result.CountAsync();

            var pagedResult = await result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagingData<List<PostViewModel>>
            {
                DataSource = pagedResult,
                Total = recordsTotal,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal
            };
        }
    }
}