
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
        public interface IRoomRepository
        {
            Task <List< Room>> List();

            Task <List< Room>> Search(string keyword);

            Task <List< Room>> ListPaging(int pageIndex, int pageSize);

            Task <Room> Detail(int ? postId);

            Task <Room> Add(Room Room);

            Task Update(Room Room);

            Task Delete(Room Room);

            Task <int> DeletePermanently(int ? RoomId);

            int Count();

            Task <DTResult<Room>> ListServerSide(RoomDTParameters parameters);
        }
    }
