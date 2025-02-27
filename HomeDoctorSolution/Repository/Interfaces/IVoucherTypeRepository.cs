
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
        public interface IVoucherTypeRepository
        {
            Task <List< VoucherType>> List();

            Task <List< VoucherType>> Search(string keyword);

            Task <List< VoucherType>> ListPaging(int pageIndex, int pageSize);

            Task <VoucherType> Detail(int ? postId);

            Task <VoucherType> Add(VoucherType VoucherType);

            Task Update(VoucherType VoucherType);

            Task Delete(VoucherType VoucherType);

            Task <int> DeletePermanently(int ? VoucherTypeId);

            int Count();

            Task <DTResult<VoucherType>> ListServerSide(VoucherTypeDTParameters parameters);
        }
    }
