
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Util.DTParameters;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Util.Entities;
using HomeDoctorSolution.Constants;
using HomeDoctor.Models.ViewModels.Post;

namespace HomeDoctorSolution.Services
{
    public class PostService : IPostService
    {
        IPostRepository postRepository;
        IPostTagRepository postTagRepository;
        ITagRepository tagRepository;
        public PostService(
            IPostRepository _postRepository, 
            IPostTagRepository _postTagRepository,
            ITagRepository _tagRepository

            )
        {
            postRepository = _postRepository;
            tagRepository = _tagRepository;
            postTagRepository = _postTagRepository;
        }
        public async Task Add(Post obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await postRepository.Add(obj);
        }

        public int Count()
        {
            var result = postRepository.Count();
            return result;
        }

        public async Task Delete(Post obj)
        {
            obj.Active = 0;
            await postRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await postRepository.DeletePermanently(id);
        }

        Task<List<Post>> IBaseService<Post>.ListPaging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> Detail(int? id)
        {
            return await postRepository.Detail(id);
        }

        public async Task<List<Post>> List()
        {
            return await postRepository.List();
        }

        public async Task<List<PostViewModel>> ListPaging(int pageIndex, int pageSize)
        {
            return await postRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<PostViewModel>> ListServerSide(PostDTParameters parameters)
        {
            return await postRepository.ListServerSide(parameters);
        }

        public async Task<List<Post>> Search(string keyword)
        {
            return await postRepository.Search(keyword);
        }

        public async Task Update(Post obj)
        {
            await postRepository.Update(obj);
        }

        public async Task UpdateByRole(Post obj, int roleId)
        {
            await postRepository.UpdateByRole(obj, roleId);
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<Post>> PostChartReportByTime(string startDate, string endDate)
        {
            return await postRepository.PostChartReportByTime(startDate, endDate);
        }

        public async Task<List<PostReport>> PostQuantityReport()
        {
            return await postRepository.PostQuantityReport();
        }

        public async Task<PagingData<List<PostViewModel>>> ListPost(PostParameters parameter)
        {
            return await postRepository.ListPost(parameter);
        }
        public async Task<object> LoadDataFilterPostHomePageAsync()
        {
            return await postRepository.LoadDataFilterPostHomePageAsync();
        }

        public Task<PostViewModel> DetailPostByIdAsync(int id)
        {
            return postRepository.DetailPostByIdAsync(id);
        }

        public async Task<PostViewModel> LatestPostsByTime()
        {
            return await postRepository.LatestPostsByTime();
        }

        public async Task<List<PostViewModel>> Top3Post()
        {
            return await postRepository.Top3Post();
        }

        public async Task<HomeDoctorResponse> CreateAsync(InsertPostDTO obj)
        {
            var listError = new List<string>();
            var isNameExit = await postRepository.IsNameExistInSameCategoryAsync(0, obj.PostCategoryId, obj.Name);
            if (isNameExit)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return HomeDoctorResponse.BAD_REQUEST(listError);
            }

            var newObj = new Post
            {

                CreatedTime = DateTime.Now,
                Active = 1,
                PostTypeId = PostContants.POST_TYPE_ID,
                PostCategoryId = obj.PostCategoryId,
                Description = obj.Description,
                AuthorId = obj.AuthorId,
                Name = obj.Name,
                Text = obj.Text,
                Photo = obj.Photo,
                PostPublishStatusId = obj.PostPublishStatusId,
                PostCommentStatusId = obj.PostCommentStatusId,
                PublishedTime = obj.PublishedTime,
                Url = obj.Url,
                PostAccountId = obj.PostAccountId,
                PostLayoutId = PostContants.POST_LAYOUT
            };

            var database = postRepository.GetDatabase();
            using var transaction = database.BeginTransaction();
            try
            {
                var addPost = await postRepository.Add(newObj);
                if (addPost.Id > 0)
                {
                    var listPostTag = new List<PostTag>();
                    foreach (var i in obj.TagIds)
                    {
                        var tagRepo = await tagRepository.Detail(int.Parse(i));
                        if (tagRepo != null)
                        {
                            listPostTag.Add(new PostTag()
                            {
                                TagId = int.Parse(i),
                                Id = 0,
                                Active = 1,
                                Name = tagRepo.Name,
                                CreatedTime = DateTime.Now,
                                PostId = addPost.Id,
                            });
                        }
                    }
                    await postTagRepository.InsertManyAsync(listPostTag);
                    await transaction.CommitAsync();
                    return HomeDoctorResponse.Success("Thêm mới bài viết thành công.");
                }
                else
                {
                    await transaction.RollbackAsync();
                    return HomeDoctorResponse.Success("Thêm mới bài viết không thành công.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return HomeDoctorResponse.Success("Thêm mới bài viết không thành công.");
            }
        }

        public Task<List<PostViewModel>> ListPostMobile(int pageIndex, int pageSize)
        {
            return postRepository.ListPostMobile(pageIndex, pageSize);
        }

        public async Task<PagingData<List<PostViewModel>>> ListPostByPostCategory(PostParameters parameter)
        {
            return await postRepository.ListPostByPostCategory(parameter);
        }

        public Task<PagingData<List<PostViewModel>>> ListPostByPostTag(PostParameters parameter)
        {
            return postRepository.ListPostByPostTag(parameter);
        }

        public Task<PagingData<List<PostViewModel>>> ListPostByPostName(PostParameters parameter)
        {
            return postRepository.ListPostByPostName(parameter);
        }
        public async Task<List<PostViewModel>> DetailById(int? id)
        {
            return await postRepository.DetailById(id);
        }
        public async Task<List<PostViewModel>> ListFourPostByTimeDesc()
        {
            return await postRepository.ListFourPostByTimeDesc();
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<PagingData<List<PostViewModel>>> ListPostHomePageAsync(SearchingPostParameters parameters)
        {
            return await postRepository.ListPostHomePageAsync(parameters);
        }
    }
}

