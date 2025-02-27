
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
        public interface ISurveyAccountRepository
        {
            Task <List< SurveyAccount>> List();

            Task <List< SurveyAccount>> Search(string keyword);

            Task <List< SurveyAccount>> ListPaging(int pageIndex, int pageSize);

            Task <SurveyAccount> Detail(int ? postId);

            Task <SurveyAccount> Add(SurveyAccount SurveyAccount);

            Task Update(SurveyAccount SurveyAccount);

            Task Delete(SurveyAccount SurveyAccount);

            Task <int> DeletePermanently(int ? SurveyAccountId);

            int Count();

            Task <DTResult<SurveyAccountViewModel>> ListServerSide(SurveyAccountDTParameters parameters);
        }
    }
