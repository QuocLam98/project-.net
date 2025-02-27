
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
        public interface ISurveyMetaRepository
        {
            Task <List< SurveyMeta>> List();

            Task <List< SurveyMeta>> Search(string keyword);

            Task <List< SurveyMeta>> ListPaging(int pageIndex, int pageSize);

            Task <SurveyMeta> Detail(int ? postId);

            Task <SurveyMeta> Add(SurveyMeta SurveyMeta);

            Task Update(SurveyMeta SurveyMeta);

            Task Delete(SurveyMeta SurveyMeta);

            Task <int> DeletePermanently(int ? SurveyMetaId);

            int Count();

            Task <DTResult<SurveyMetaViewModel>> ListServerSide(SurveyMetaDTParameters parameters);
        }
    }
