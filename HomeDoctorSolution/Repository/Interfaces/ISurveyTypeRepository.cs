
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
        public interface ISurveyTypeRepository
        {
            Task <List< SurveyType>> List();

            Task <List< SurveyType>> Search(string keyword);

            Task <List< SurveyType>> ListPaging(int pageIndex, int pageSize);

            Task <SurveyType> Detail(int ? postId);

            Task <SurveyType> Add(SurveyType SurveyType);

            Task Update(SurveyType SurveyType);

            Task Delete(SurveyType SurveyType);

            Task <int> DeletePermanently(int ? SurveyTypeId);

            int Count();

            Task <DTResult<SurveyType>> ListServerSide(SurveyTypeDTParameters parameters);
        }
    }
