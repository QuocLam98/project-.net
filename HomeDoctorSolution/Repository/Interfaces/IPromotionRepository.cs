
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
        public interface IPromotionRepository
        {
            Task <List< Promotion>> List();

            Task <List< Promotion>> Search(string keyword);

            Task <List< Promotion>> ListPaging(int pageIndex, int pageSize);

            Task <Promotion> Detail(int ? postId);

            Task <Promotion> Add(Promotion Promotion);

            Task Update(Promotion Promotion);

            Task Delete(Promotion Promotion);

            Task <int> DeletePermanently(int ? PromotionId);

            int Count();

            Task <DTResult<Promotion>> ListServerSide(PromotionDTParameters parameters);
        }
    }
