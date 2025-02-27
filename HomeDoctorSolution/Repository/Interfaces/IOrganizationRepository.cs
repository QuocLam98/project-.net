
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
        public interface IOrganizationRepository
        {
            Task <List< Organization>> List();

            Task <List< Organization>> Search(string keyword);

            Task <List< Organization>> ListPaging(int pageIndex, int pageSize);

            Task <Organization> Detail(int ? postId);

            Task <Organization> Add(Organization Organization);

            Task Update(Organization Organization);

            Task Delete(Organization Organization);

            Task <int> DeletePermanently(int ? OrganizationId);

            int Count();

            Task <DTResult<OrganizationViewModel>> ListServerSide(OrganizationDTParameters parameters);
        }
    }
