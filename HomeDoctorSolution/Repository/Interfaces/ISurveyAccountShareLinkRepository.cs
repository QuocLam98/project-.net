
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
        public interface ISurveyAccountShareLinkRepository
        {
            Task <List< SurveyAccountShareLink>> List();

            Task <List< SurveyAccountShareLink>> Search(string keyword);

            Task <List< SurveyAccountShareLink>> ListPaging(int pageIndex, int pageSize);

            Task <SurveyAccountShareLink> Detail(int ? postId);

            Task <SurveyAccountShareLink> Add(SurveyAccountShareLink SurveyAccountShareLink);

            Task Update(SurveyAccountShareLink SurveyAccountShareLink);

            Task Delete(SurveyAccountShareLink SurveyAccountShareLink);

            Task <int> DeletePermanently(int ? SurveyAccountShareLinkId);

            int Count();

            Task <DTResult<SurveyAccountShareLink>> ListServerSide(SurveyAccountShareLinkDTParameters parameters);
        }
    }
