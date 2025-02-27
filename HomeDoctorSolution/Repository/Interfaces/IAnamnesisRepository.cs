
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
    public interface IAnamnesisRepository
    {
        Task<List<Anamnesis>> List();

        Task<List<Anamnesis>> Search(string keyword);

        Task<List<Anamnesis>> ListPaging(int pageIndex, int pageSize);

        Task<Anamnesis> Detail(int? postId);

        Task<Anamnesis> Add(Anamnesis Anamnesis);

        Task Update(Anamnesis Anamnesis);

        Task Delete(Anamnesis Anamnesis);

        Task<int> DeletePermanently(int? AnamnesisId);

        int Count();

        Task<DTResult<AnamnesisViewModel>> ListServerSide(AnamnesisDTParameters parameters);
        Task<bool> CheckExistByAccountId(int? accountId);
        Task<List<Anamnesis>> DetailByAccountId(int? accountId);
    }
}
