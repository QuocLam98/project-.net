
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
        public interface ISurveySectionRepository
        {
            Task <List< SurveySection>> List();

            Task <List< SurveySection>> Search(string keyword);

            Task <List< SurveySection>> ListPaging(int pageIndex, int pageSize);

            Task <SurveySection> Detail(int ? postId);

            Task <SurveySection> Add(SurveySection SurveySection);

            Task Update(SurveySection SurveySection);

            Task Delete(SurveySection SurveySection);

            Task <int> DeletePermanently(int ? SurveySectionId);

            int Count();

            Task <DTResult<SurveySectionViewModel>> ListServerSide(SurveySectionDTParameters parameters);
        }
    }
