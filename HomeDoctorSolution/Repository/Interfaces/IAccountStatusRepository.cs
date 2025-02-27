
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
        public interface IAccountStatusRepository
        {
            Task <List< AccountStatus>> List();

            Task <List< AccountStatus>> Search(string keyword);

            Task <List< AccountStatus>> ListPaging(int pageIndex, int pageSize);

            Task <AccountStatus> Detail(int ? postId);

            Task <AccountStatus> Add(AccountStatus AccountStatus);

            Task Update(AccountStatus AccountStatus);

            Task Delete(AccountStatus AccountStatus);

            Task <int> DeletePermanently(int ? AccountStatusId);

            int Count();

            Task <DTResult<AccountStatus>> ListServerSide(AccountStatusDTParameters parameters);
        }
    }
