
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
        public interface IContactStatusRepository
        {
            Task <List< ContactStatus>> List();

            Task <List< ContactStatus>> Search(string keyword);

            Task <List< ContactStatus>> ListPaging(int pageIndex, int pageSize);

            Task <ContactStatus> Detail(int ? postId);

            Task <ContactStatus> Add(ContactStatus ContactStatus);

            Task Update(ContactStatus ContactStatus);

            Task Delete(ContactStatus ContactStatus);

            Task <int> DeletePermanently(int ? ContactStatusId);

            int Count();

            Task <DTResult<ContactStatus>> ListServerSide(ContactStatusDTParameters parameters);
        }
    }
