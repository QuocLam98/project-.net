
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
        public interface IEntityRepository
        {
            Task <List< Entity>> List();

            Task <List< Entity>> Search(string keyword);

            Task <List< Entity>> ListPaging(int pageIndex, int pageSize);

            Task <Entity> Detail(int ? postId);

            Task <Entity> Add(Entity Entity);

            Task Update(Entity Entity);

            Task Delete(Entity Entity);

            Task <int> DeletePermanently(int ? EntityId);

            int Count();

            Task <DTResult<Entity>> ListServerSide(EntityDTParameters parameters);
        }
    }
