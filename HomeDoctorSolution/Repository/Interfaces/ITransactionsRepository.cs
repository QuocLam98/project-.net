
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
        public interface ITransactionsRepository
        {
            Task <List< Transaction>> List();

            Task <List< Transaction>> Search(string keyword);

            Task <List< Transaction>> ListPaging(int pageIndex, int pageSize);

            Task <Transaction> Detail(int ? postId);

            Task <Transaction> Add(Transaction Transaction);

            Task Update(Transaction Transaction);

            Task Delete(Transaction Transaction);

            Task <int> DeletePermanently(int ? TransactionsId);

            int Count();

            Task <DTResult<TransactionsViewModel>> ListServerSide(TransactionsDTParameters parameters);
        }
    }
