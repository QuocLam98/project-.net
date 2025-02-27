
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
        public interface ISystemConfigRepository
        {
            Task <List< SystemConfig>> List();

            Task <List< SystemConfig>> Search(string keyword);

            Task <List< SystemConfig>> ListPaging(int pageIndex, int pageSize);

            Task <SystemConfig> Detail(int ? postId);

            Task <SystemConfig> Add(SystemConfig SystemConfig);

            Task Update(SystemConfig SystemConfig);

            Task Delete(SystemConfig SystemConfig);

            Task <int> DeletePermanently(int ? SystemConfigId);

            int Count();

            Task <DTResult<SystemConfig>> ListServerSide(SystemConfigDTParameters parameters);
        }
    }
