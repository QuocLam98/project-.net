
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
        public interface IOrganizationTypeRepository
        {
            Task <List< OrganizationType>> List();

            Task <List< OrganizationType>> Search(string keyword);

            Task <List< OrganizationType>> ListPaging(int pageIndex, int pageSize);

            Task <OrganizationType> Detail(int ? postId);

            Task <OrganizationType> Add(OrganizationType OrganizationType);

            Task Update(OrganizationType OrganizationType);

            Task Delete(OrganizationType OrganizationType);

            Task <int> DeletePermanently(int ? OrganizationTypeId);

            int Count();

            Task <DTResult<OrganizationType>> ListServerSide(OrganizationTypeDTParameters parameters);
        }
    }
