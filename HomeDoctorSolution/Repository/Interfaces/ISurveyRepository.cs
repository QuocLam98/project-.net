
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
        public interface ISurveyRepository
        {
            Task <List< Survey>> List();

            Task <List< Survey>> Search(string keyword);

            Task <List< Survey>> ListPaging(int pageIndex, int pageSize);

            Task <Survey> Detail(int ? postId);

            Task <Survey> Add(Survey Survey);

            Task Update(Survey Survey);

            Task Delete(Survey Survey);

            Task <int> DeletePermanently(int ? SurveyId);

            int Count();

            Task <DTResult<SurveyViewModel>> ListServerSide(SurveyDTParameters parameters);
        }
    }
