
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
        public interface IVoucherStatusRepository
        {
            Task <List< VoucherStatus>> List();

            Task <List< VoucherStatus>> Search(string keyword);

            Task <List< VoucherStatus>> ListPaging(int pageIndex, int pageSize);

            Task <VoucherStatus> Detail(int ? postId);

            Task <VoucherStatus> Add(VoucherStatus VoucherStatus);

            Task Update(VoucherStatus VoucherStatus);

            Task Delete(VoucherStatus VoucherStatus);

            Task <int> DeletePermanently(int ? VoucherStatusId);

            int Count();

            Task <DTResult<VoucherStatus>> ListServerSide(VoucherStatusDTParameters parameters);
        }
    }
