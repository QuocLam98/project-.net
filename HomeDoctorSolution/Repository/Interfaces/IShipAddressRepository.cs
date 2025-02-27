
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
        public interface IShipAddressRepository
        {
            Task <List< ShipAddress>> List();

            Task <List< ShipAddress>> Search(string keyword);

            Task <List< ShipAddress>> ListPaging(int pageIndex, int pageSize);

            Task <ShipAddress> Detail(int ? postId);

            Task <ShipAddress> Add(ShipAddress ShipAddress);

            Task Update(ShipAddress ShipAddress);

            Task Delete(ShipAddress ShipAddress);

            Task <int> DeletePermanently(int ? ShipAddressId);

            int Count();

            Task <DTResult<ShipAddressViewModel>> ListServerSide(ShipAddressDTParameters parameters);
        }
    }
