
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
        public interface IOrganizationStatusRepository
        {
            Task <List< OrganizationStatus>> List();

            Task <List< OrganizationStatus>> Search(string keyword);

            Task <List< OrganizationStatus>> ListPaging(int pageIndex, int pageSize);

            Task <OrganizationStatus> Detail(int ? postId);

            Task <OrganizationStatus> Add(OrganizationStatus OrganizationStatus);

            Task Update(OrganizationStatus OrganizationStatus);

            Task Delete(OrganizationStatus OrganizationStatus);

            Task <int> DeletePermanently(int ? OrganizationStatusId);

            int Count();

            Task <DTResult<OrganizationStatus>> ListServerSide(OrganizationStatusDTParameters parameters);
        }
    }
