
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
        public interface ISurveyStatusRepository
        {
            Task <List< SurveyStatus>> List();

            Task <List< SurveyStatus>> Search(string keyword);

            Task <List< SurveyStatus>> ListPaging(int pageIndex, int pageSize);

            Task <SurveyStatus> Detail(int ? postId);

            Task <SurveyStatus> Add(SurveyStatus SurveyStatus);

            Task Update(SurveyStatus SurveyStatus);

            Task Delete(SurveyStatus SurveyStatus);

            Task <int> DeletePermanently(int ? SurveyStatusId);

            int Count();

            Task <DTResult<SurveyStatus>> ListServerSide(SurveyStatusDTParameters parameters);
        }
    }
