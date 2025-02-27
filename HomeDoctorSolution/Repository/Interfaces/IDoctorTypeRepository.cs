
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
        public interface IDoctorTypeRepository
        {
            Task <List< DoctorType>> List();

            Task <List< DoctorType>> Search(string keyword);

            Task <List< DoctorType>> ListPaging(int pageIndex, int pageSize);

            Task <DoctorType> Detail(int ? postId);

            Task <DoctorType> Add(DoctorType DoctorType);

            Task Update(DoctorType DoctorType);

            Task Delete(DoctorType DoctorType);

            Task <int> DeletePermanently(int ? DoctorTypeId);

            int Count();

            Task <DTResult<DoctorType>> ListServerSide(DoctorTypeDTParameters parameters);
        }
    }
