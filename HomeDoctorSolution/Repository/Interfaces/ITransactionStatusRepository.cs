
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
        public interface ITransactionStatusRepository
        {
            Task <List< TransactionStatus>> List();

            Task <List< TransactionStatus>> Search(string keyword);

            Task <List< TransactionStatus>> ListPaging(int pageIndex, int pageSize);

            Task <TransactionStatus> Detail(int ? postId);

            Task <TransactionStatus> Add(TransactionStatus TransactionStatus);

            Task Update(TransactionStatus TransactionStatus);

            Task Delete(TransactionStatus TransactionStatus);

            Task <int> DeletePermanently(int ? TransactionStatusId);

            int Count();

            Task <DTResult<TransactionStatus>> ListServerSide(TransactionStatusDTParameters parameters);
        }
    }
