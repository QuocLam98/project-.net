
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
        public interface IAccountTypeRepository
        {
            Task <List< AccountType>> List();

            Task <List< AccountType>> Search(string keyword);

            Task <List< AccountType>> ListPaging(int pageIndex, int pageSize);

            Task <AccountType> Detail(int ? postId);

            Task <AccountType> Add(AccountType AccountType);

            Task Update(AccountType AccountType);

            Task Delete(AccountType AccountType);

            Task <int> DeletePermanently(int ? AccountTypeId);

            int Count();

            Task <DTResult<AccountType>> ListServerSide(AccountTypeDTParameters parameters);
        }
    }
