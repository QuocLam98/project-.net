using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Models.UploadFileModel;

namespace HomeDoctorSolution.Services.UploadFile.Interfaces
{
    public interface IFileExplorerService
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Get all folder
        /// </summary>
        /// <returns></returns>
        Task<HomeDoctorResponse> GetAllFolders();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Add folder
        /// </summary>
        /// <param name="obj">insert folder upload DTO</param>
        /// <returns></returns>
        Task<HomeDoctorResponse> AddFolder(InsertFolderUploadDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: Save file to server
        /// </summary>
        /// <param name="obj">insert upload file object</param>
        /// <returns></returns>
        Task<HomeDoctorResponse> SaveFile(InsertUploadFileDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: save large file
        /// </summary>
        /// <param name="obj">insert larger upload file DTO object</param>
        /// <returns></returns>
        Task<HomeDoctorResponse> SaveLargeFile(InsertLargeUploadFileDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: get list paging upload file
        /// </summary>
        /// <param name="parameters">paging upload file parameters</param>
        /// <returns></returns>
        Task<HomeDoctorResponse> ListPagingFile(PagingUploadFileParameter parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 01/08/2023
        /// Description: Current usre upload file
        /// </summary>
        /// <param name="obj">insert upload file DTO</param>
        /// <param name="user">type user upload</param>
        /// <param name="type">type file upload</param>
        /// <returns></returns>
        Task<HomeDoctorResponse> CurrentUserSaveFile(InsertUploadFileDTO obj, string user, string type);

        Task<HomeDoctorResponse> DeleteFile(DeleteFileDTO obj);

        Task<HomeDoctorResponse> GetAllFoldersByAccountId(int accountId);

        Task<HomeDoctorResponse> ListPagingFileByAccountId(PagingUploadFileParameter parameters, int accountId);
        Task<List<UploadFiles>> SaveFile2(InsertUploadFileDTO obj);
        Task<List<UploadFiles>> SaveLargeFile2(InsertLargeUploadFileDTO obj);
    }
}