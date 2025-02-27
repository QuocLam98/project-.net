using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository.UploadFile.Interfaces;

namespace HomeDoctorSolution.Repository.UploadFile
{
    public class FolderUploadRepository : RepositoryBaseAsync<FolderUpload, int, HomeDoctorContext>, IFolderUploadRepository
    {
        private readonly HomeDoctorContext _db;
        private readonly IUnitOfWork<HomeDoctorContext> _unitOfWork;

        public FolderUploadRepository(IUnitOfWork<HomeDoctorContext> unitOfWork, HomeDoctorContext db) : base(db, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
    }
}
