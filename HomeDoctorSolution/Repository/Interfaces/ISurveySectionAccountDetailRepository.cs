
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
        public interface ISurveySectionAccountDetailRepository
        {
            Task <List< SurveySectionAccountDetail>> List();

            Task <List< SurveySectionAccountDetail>> Search(string keyword);

            Task <List< SurveySectionAccountDetail>> ListPaging(int pageIndex, int pageSize);

            Task <SurveySectionAccountDetail> Detail(int ? postId);

            Task <SurveySectionAccountDetail> Add(SurveySectionAccountDetail SurveySectionAccountDetail);

            Task Update(SurveySectionAccountDetail SurveySectionAccountDetail);

            Task Delete(SurveySectionAccountDetail SurveySectionAccountDetail);

            Task <int> DeletePermanently(int ? SurveySectionAccountDetailId);

            int Count();

            Task <DTResult<SurveySectionAccountDetailViewModel>> ListServerSide(SurveySectionAccountDetailDTParameters parameters);
        }
    }
