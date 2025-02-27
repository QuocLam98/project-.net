

using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Models.UploadFileModel;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace HomeDoctorSolution.Services.UploadFile.Interfaces
{
    public interface IFileStorageService
    {
        Task<UploadFiles> SaveFile(IFormFile file, string filePath);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Upload many file 
        /// </summary>
        /// <param name="obj">insert upload file DTO</param>
        /// <param name="rootPath">folder upload full path</param>
        /// <returns></returns>
        public Task<List<UploadFiles>> SaveManyFile(InsertUploadFileDTO obj, string rootPath);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: add folder
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        string AddFolder(string folderPath);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Save image
        /// </summary>
        /// <param name="file">image stream</param>
        /// <param name="filePath">file full path</param>
        /// <param name="thumbPath">thumb path</param>
        /// <returns></returns>
        Task<UploadFiles> SaveImage(IFormFile file, string filePath);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30//0
        /// Description: Created thumbnail//funciton only run in using stream or file
        /// </summary>
        /// <param name="filePath">file image full path</param>
        /// <param name="image">image</param>
        Task<string> CreateThumbnail(string filePath, Image image);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: upload large file
        /// </summary>
        /// <param name="obj">insert large file objectS</param>
        /// <param name="rootFolder">folder root path</param>
        /// <returns></returns>
        Task<List<UploadFiles>> SaveLargeFile(InsertLargeUploadFileDTO obj, string rootFolder);
        Task<long> SaveLagreFileAsync(FileMultipartSection fileSection, string filePath);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 12/06/2023
        /// Description: Get boundary of file
        /// </summary>
        /// <param name="contentType">content tyoe</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        string GetBoundary(MediaTypeHeaderValue contentType);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 01/08/2023
        /// Description: Upload cover photo
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        Task<List<UploadFiles>> SaveCoverPhoto(InsertUploadFileDTO obj, string rootPath);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 01/08/2023
        /// Description: Upload cover avatar
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        Task<List<UploadFiles>> SaveAvartar(InsertUploadFileDTO obj, string rootPath);

        Task<List<int>> DeleteFile(DeleteFileDTO obj, string rootPath);

	}
}
