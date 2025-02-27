using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.UploadFileModel;
using HomeDoctorSolution.Repository.Interfaces;
using HomeDoctorSolution.Util.Entities;

namespace HomeDoctorSolution.Repository.UploadFile.Interfaces
{
    public interface IUploadFileRepository : IRepositoryBaseAsync<UploadFiles, int, HomeDoctorContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: get list paging upload file
        /// </summary>
        /// <param name="parameters">paging upload file parameters</param>
        /// <returns></returns>
        Task<PagingData<List<UploadFiles>>> ListPaging(PagingUploadFileParameter parameters);
        Task<PagingData<List<UploadFiles>>> ListPagingByAccountId(PagingUploadFileParameter parameters, int accountId);
    }
}
