
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Repository
{
    public interface ITagRepository
    {
        Task<List<Tag>> List();

        Task<List<Tag>> Search(string keyword);

        Task<List<Tag>> ListPaging(int pageIndex, int pageSize);

        Task<Tag> Detail(int? postId);

        Task<Tag> Add(Tag Tag);

        Task Update(Tag Tag);

        Task Delete(Tag Tag);

        Task<int> DeletePermanently(int? TagId);

        int Count();

        Task<DTResult<Tag>> ListServerSide(TagDTParameters parameters);
        Task<object> ListSelectTagAsync(TagForSelect2Aggregates obj);

        Task<HomeDoctorResponse> AddTagForPostAsync(InsertTagDTO obj);
    }
}
