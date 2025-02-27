
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
        public interface IEmailRepository
        {
            Task <List< Email>> List();

            Task <List< Email>> Search(string keyword);

            Task <List< Email>> ListPaging(int pageIndex, int pageSize);

            Task <Email> Detail(int ? postId);

            Task <Email> Add(Email Email);

            Task Update(Email Email);

            Task Delete(Email Email);

            Task <int> DeletePermanently(int ? EmailId);

            int Count();

            Task <DTResult<Email>> ListServerSide(EmailDTParameters parameters);
        }
    }
