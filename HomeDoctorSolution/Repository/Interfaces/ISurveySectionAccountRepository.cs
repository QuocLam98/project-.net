
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
        public interface ISurveySectionAccountRepository
        {
            Task <List< SurveySectionAccount>> List();

            Task <List< SurveySectionAccount>> Search(string keyword);

            Task <List< SurveySectionAccount>> ListPaging(int pageIndex, int pageSize);

            Task <SurveySectionAccount> Detail(int ? postId);

            Task <SurveySectionAccount> Add(SurveySectionAccount SurveySectionAccount);

            Task Update(SurveySectionAccount SurveySectionAccount);

            Task Delete(SurveySectionAccount SurveySectionAccount);

            Task <int> DeletePermanently(int ? SurveySectionAccountId);

            int Count();

            Task <DTResult<SurveySectionAccountViewModel>> ListServerSide(SurveySectionAccountDTParameters parameters);
        }
    }
