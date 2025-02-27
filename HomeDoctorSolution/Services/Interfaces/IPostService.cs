
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctor.Util.DTParameters;
using HomeDoctorSolution.Util.Entities;
using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctor.Models.ViewModels.Post;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IPostService : IBaseService<Post>
    {
        Task<DTResult<PostViewModel>> ListServerSide(PostDTParameters parameters);
        /// <summary>
        /// Author: JinDo
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<Post>> PostChartReportByTime(string startDate, string endDate);

        /// <summary>
        /// Author: JinDo
        /// </summary>
        /// <returns></returns>
        Task<List<PostReport>> PostQuantityReport();

        Task<PagingData<List<PostViewModel>>> ListPost(PostParameters parameter);

        Task<object> LoadDataFilterPostHomePageAsync();

        Task<HomeDoctorResponse> CreateAsync(InsertPostDTO obj);
        Task<PostViewModel> DetailPostByIdAsync(int id);

        Task<PostViewModel> LatestPostsByTime();
        Task<List<PostViewModel>> Top3Post();

        Task<List<PostViewModel>> ListPostMobile(int pageIndex, int pageSize);

        Task<PagingData<List<PostViewModel>>> ListPostByPostCategory(PostParameters parameter);
        Task<PagingData<List<PostViewModel>>> ListPostByPostTag(PostParameters parameter);

        Task<PagingData<List<PostViewModel>>> ListPostByPostName(PostParameters parameter);
        Task<List<PostViewModel>> DetailById(int? postId);
        //End : Library
        Task<List<PostViewModel>> ListFourPostByTimeDesc();
        new Task<List<PostViewModel>> ListPaging(int pageIndex, int pageSize);

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<PagingData<List<PostViewModel>>> ListPostHomePageAsync(SearchingPostParameters parameters);
    }
}
