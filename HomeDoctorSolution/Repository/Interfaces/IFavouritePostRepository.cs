
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
        public interface IFavouritePostRepository
        {
            Task <List< FavouritePost>> List();

            Task <List< FavouritePost>> Search(string keyword);

            Task <List< FavouritePost>> ListPaging(int pageIndex, int pageSize);

            Task <FavouritePost> Detail(int ? postId);

            Task <FavouritePost> Add(FavouritePost FavouritePost);

            Task Update(FavouritePost FavouritePost);

            Task Delete(FavouritePost FavouritePost);

            Task <int> DeletePermanently(int ? FavouritePostId);

            int Count();

            Task <DTResult<FavouritePostViewModel>> ListServerSide(FavouritePostDTParameters parameters);
        }
    }
