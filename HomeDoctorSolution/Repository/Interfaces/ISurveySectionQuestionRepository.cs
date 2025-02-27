
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
        public interface ISurveySectionQuestionRepository
        {
            Task <List< SurveySectionQuestion>> List();

            Task <List< SurveySectionQuestion>> Search(string keyword);

            Task <List< SurveySectionQuestion>> ListPaging(int pageIndex, int pageSize);

            Task <SurveySectionQuestion> Detail(int ? postId);

            Task <SurveySectionQuestion> Add(SurveySectionQuestion SurveySectionQuestion);

            Task Update(SurveySectionQuestion SurveySectionQuestion);

            Task Delete(SurveySectionQuestion SurveySectionQuestion);

            Task <int> DeletePermanently(int ? SurveySectionQuestionId);

            int Count();

            Task <DTResult<SurveySectionQuestionViewModel>> ListServerSide(SurveySectionQuestionDTParameters parameters);
        }
    }
