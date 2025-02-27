using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository.Interfaces;

namespace HomeDoctorSolution.Repository.UploadFile.Interfaces
{
    public interface IFolderUploadRepository : IRepositoryBaseAsync<FolderUpload, int, HomeDoctorContext>
    {

    }
}
