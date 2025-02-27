
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
        public interface IAuthorRepository
        {
            Task <List< Author>> List();

            Task <List< Author>> Search(string keyword);

            Task <List< Author>> ListPaging(int pageIndex, int pageSize);

            Task <Author> Detail(int ? postId);

            Task <Author> Add(Author Author);

            Task Update(Author Author);

            Task Delete(Author Author);

            Task <int> DeletePermanently(int ? AuthorId);

            int Count();

            Task <DTResult<AuthorViewModel>> ListServerSide(AuthorDTParameters parameters);
        }
    }
