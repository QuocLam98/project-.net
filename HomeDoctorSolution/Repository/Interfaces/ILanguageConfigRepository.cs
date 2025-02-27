
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
        public interface ILanguageConfigRepository
        {
            Task <List< LanguageConfig>> List();

            Task <List< LanguageConfig>> Search(string keyword);

            Task <List< LanguageConfig>> ListPaging(int pageIndex, int pageSize);

            Task <LanguageConfig> Detail(int ? postId);

            Task <LanguageConfig> Add(LanguageConfig LanguageConfig);

            Task Update(LanguageConfig LanguageConfig);

            Task Delete(LanguageConfig LanguageConfig);

            Task <int> DeletePermanently(int ? LanguageConfigId);

            int Count();

            Task <DTResult<LanguageConfig>> ListServerSide(LanguageConfigDTParameters parameters);
        }
    }
