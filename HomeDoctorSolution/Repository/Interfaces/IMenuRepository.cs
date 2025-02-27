
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
        public interface IMenuRepository
        {
            Task <List< Menu>> List();

            Task <List< Menu>> Search(string keyword);

            Task <List< Menu>> ListPaging(int pageIndex, int pageSize);

            Task <Menu> Detail(int ? postId);

            Task <Menu> Add(Menu Menu);

            Task Update(Menu Menu);

            Task Delete(Menu Menu);

            Task <int> DeletePermanently(int ? MenuId);

            int Count();

            Task <DTResult<MenuViewModel>> ListServerSide(MenuDTParameters parameters);
        }
    }
