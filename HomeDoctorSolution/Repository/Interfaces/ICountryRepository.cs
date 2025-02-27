
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
        public interface ICountryRepository
        {
            Task <List< Country>> List();

            Task <List< Country>> Search(string keyword);

            Task <List< Country>> ListPaging(int pageIndex, int pageSize);

            Task <Country> Detail(int ? postId);

            Task <Country> Add(Country Country);

            Task Update(Country Country);

            Task Delete(Country Country);

            Task <int> DeletePermanently(int ? CountryId);

            int Count();

            Task <DTResult<Country>> ListServerSide(CountryDTParameters parameters);
        }
    }
