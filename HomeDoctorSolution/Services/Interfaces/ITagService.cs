
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface ITagService : IBaseService<Tag>
    {
        Task<DTResult<Tag>> ListServerSide(TagDTParameters parameters);
        Task<object> ListSelectTagAsync(TagForSelect2Aggregates obj);
        Task<HomeDoctorResponse> AddTagForPostAsync(InsertTagDTO obj);
    }
}
