
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
        public interface IVoucherRepository
        {
            Task <List< Voucher>> List();

            Task <List< Voucher>> Search(string keyword);

            Task <List< Voucher>> ListPaging(int pageIndex, int pageSize);

            Task <Voucher> Detail(int ? postId);

            Task <Voucher> Add(Voucher Voucher);

            Task Update(Voucher Voucher);

            Task Delete(Voucher Voucher);

            Task <int> DeletePermanently(int ? VoucherId);

            int Count();

            Task <DTResult<VoucherViewModel>> ListServerSide(VoucherDTParameters parameters);
        }
    }
