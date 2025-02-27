
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctor.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HomeDoctor.Util.DTParameters;
using HomeDoctorSolution.Util.Entities;
using HomeDoctor.Models.ViewModels.Post;


namespace HomeDoctorSolution.Repository
{
    public interface IPostRepository
    {
        
        Task<List<Post>> List();

        Task<List<Post>> Search(string keyword);

        Task<List<PostViewModel>> ListPaging(int pageIndex, int pageSize);

        Task<Post> Detail(int? postId);

        Task<Post> Add(Post Post);

        Task Update(Post Post);

        Task Delete(Post Post);

        Task<int> DeletePermanently(int? PostId);

        int Count();

        Task<DTResult<PostViewModel>> ListServerSide(PostDTParameters parameters);

        /// <summary>
        /// Author: JinDo
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<Post>> PostChartReportByTime(string startDate, string endDate);
        Task<List<PostReport>> PostQuantityReport();
        Task<object> LoadDataFilterPostHomePageAsync();
        Task<bool> IsNameExistInSameCategoryAsync(int id, int postCategoryId, string name);
        Task<PostViewModel> DetailPostByIdAsync(int id);
        Task<PostViewModel> LatestPostsByTime();
        Task<List<PostViewModel>> Top3Post();
        Task<List<PostViewModel>> ListPostMobile(int pageIndex, int pageSize);
        DatabaseFacade GetDatabase();
        Task<bool> CheckNameIsActive(string name, int id);


        Task<PagingData<List<PostViewModel>>> ListPostByPostCategory(PostParameters parameter);
        Task<PagingData<List<PostViewModel>>> ListPostByPostTag(PostParameters parameter);
        Task<PagingData<List<PostViewModel>>> ListPostByPostName(PostParameters parameter);

        Task UpdateByRole(Post obj, int roleId);
        Task<List<PostViewModel>> DetailById(int? id);
        Task<PagingData<List<PostViewModel>>> ListPost(PostParameters parameter);


        Task<List<PostViewModel>> ListFourPostByTimeDesc();
        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<PagingData<List<PostViewModel>>> ListPostHomePageAsync(SearchingPostParameters parameters);

    }
}
