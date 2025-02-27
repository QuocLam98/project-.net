
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
        public interface ISubscribeRepository
        {
            Task <List< Subscribe>> List();

            Task <List< Subscribe>> Search(string keyword);

            Task <List< Subscribe>> ListPaging(int pageIndex, int pageSize);

            Task <Subscribe> Detail(int ? postId);

            Task <Subscribe> Add(Subscribe Subscribe);

            Task Update(Subscribe Subscribe);

            Task Delete(Subscribe Subscribe);

            Task <int> DeletePermanently(int ? SubscribeId);

            int Count();

            Task <DTResult<Subscribe>> ListServerSide(SubscribeDTParameters parameters);
        }
    }
