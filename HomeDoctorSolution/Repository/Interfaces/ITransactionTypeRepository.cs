
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
        public interface ITransactionTypeRepository
        {
            Task <List< TransactionType>> List();

            Task <List< TransactionType>> Search(string keyword);

            Task <List< TransactionType>> ListPaging(int pageIndex, int pageSize);

            Task <TransactionType> Detail(int ? postId);

            Task <TransactionType> Add(TransactionType TransactionType);

            Task Update(TransactionType TransactionType);

            Task Delete(TransactionType TransactionType);

            Task <int> DeletePermanently(int ? TransactionTypeId);

            int Count();

            Task <DTResult<TransactionType>> ListServerSide(TransactionTypeDTParameters parameters);
        }
    }
