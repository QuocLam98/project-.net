
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
        public interface IDoctorStatusRepository
        {
            Task <List< DoctorStatus>> List();

            Task <List< DoctorStatus>> Search(string keyword);

            Task <List< DoctorStatus>> ListPaging(int pageIndex, int pageSize);

            Task <DoctorStatus> Detail(int ? postId);

            Task <DoctorStatus> Add(DoctorStatus DoctorStatus);

            Task Update(DoctorStatus DoctorStatus);

            Task Delete(DoctorStatus DoctorStatus);

            Task <int> DeletePermanently(int ? DoctorStatusId);

            int Count();

            Task <DTResult<DoctorStatus>> ListServerSide(DoctorStatusDTParameters parameters);
        }
    }
