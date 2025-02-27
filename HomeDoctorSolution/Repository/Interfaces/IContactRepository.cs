
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
        public interface IContactRepository
        {
            Task <List< Contact>> List();

            Task <List< Contact>> Search(string keyword);

            Task <List< Contact>> ListPaging(int pageIndex, int pageSize);

            Task <Contact> Detail(int ? postId);

            Task <Contact> Add(Contact Contact);

            Task Update(Contact Contact);

            Task Delete(Contact Contact);

            Task <int> DeletePermanently(int ? ContactId);

            int Count();

            Task <DTResult<ContactViewModel>> ListServerSide(ContactDTParameters parameters);
        }
    }
