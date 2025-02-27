
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;


namespace HomeDoctorSolution.Repository
{
    public interface IPostTagRepository
    {
        Task<List<PostTag>> List();

        Task<List<PostTag>> Search(string keyword);

        Task<List<PostTag>> ListPaging(int pageIndex, int pageSize);

        Task<PostTag> Detail(int? postId);

        Task<PostTag> Add(PostTag PostTag);

        Task Update(PostTag PostTag);

        Task Delete(PostTag PostTag);

        Task<int> DeletePermanently(int? PostTagId);

        int Count();

        Task<DTResult<PostTagViewModel>> ListServerSide(PostTagDTParameters parameters);
        Task InsertManyAsync(IEnumerable<PostTag> objs);
    }
}
