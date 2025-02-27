
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
        public interface IMenuTypeRepository
        {
            Task <List< MenuType>> List();

            Task <List< MenuType>> Search(string keyword);

            Task <List< MenuType>> ListPaging(int pageIndex, int pageSize);

            Task <MenuType> Detail(int ? postId);

            Task <MenuType> Add(MenuType MenuType);

            Task Update(MenuType MenuType);

            Task Delete(MenuType MenuType);

            Task <int> DeletePermanently(int ? MenuTypeId);

            int Count();

            Task <DTResult<MenuType>> ListServerSide(MenuTypeDTParameters parameters);
        }
    }
