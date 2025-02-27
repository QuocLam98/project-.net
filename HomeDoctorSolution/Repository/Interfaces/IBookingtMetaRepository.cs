
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
        public interface IBookingtMetaRepository
        {
            Task <List< BookingtMeta>> List();

            Task <List< BookingtMeta>> Search(string keyword);

            Task <List< BookingtMeta>> ListPaging(int pageIndex, int pageSize);

            Task <BookingtMeta> Detail(int ? postId);

            Task <BookingtMeta> Add(BookingtMeta BookingtMeta);

            Task Update(BookingtMeta BookingtMeta);

            Task Delete(BookingtMeta BookingtMeta);

            Task <int> DeletePermanently(int ? BookingtMetaId);

            int Count();

            Task <DTResult<BookingtMetaViewModel>> ListServerSide(BookingtMetaDTParameters parameters);
        }
    }
