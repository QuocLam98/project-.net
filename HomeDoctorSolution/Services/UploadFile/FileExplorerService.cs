using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Models.UploadFileModel;
using HomeDoctorSolution.Repository.UploadFile.Interfaces;
using HomeDoctorSolution.Services.UploadFile.Interfaces;
using HomeDoctorSolution.Util;

namespace HomeDoctorSolution.Services.UploadFile
{
    public class FileExplorerService : IFileExplorerService
    {
        private readonly IFolderUploadRepository _folderUploadRepository;
        private readonly IUploadFileRepository _uploadFileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork<HomeDoctorContext> _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger _logger;
        public FileExplorerService(IFolderUploadRepository folderUploadRepository, IWebHostEnvironment webHostEnvironment, IUploadFileRepository uploadFileRepository, IUnitOfWork<HomeDoctorContext> unitOfWork, ILoggerFactory loggerFactory, IFileStorageService fileStorageService)
        {
            _folderUploadRepository = folderUploadRepository;
            _webHostEnvironment = webHostEnvironment;
            _uploadFileRepository = uploadFileRepository;
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<FileExplorerService>();
            _fileStorageService = fileStorageService;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: add folder upload
        /// </summary>
        /// <param name="obj">insert folder upload DTO object</param>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> AddFolder(InsertFolderUploadDTO obj)
        {
            FolderUpload? parent = null;
            List<string> errors = new();
            string rootFolderPath = GetRootFolderPath();
            if (obj.ParentId != null)
            {
                parent = await _folderUploadRepository.GetByIdAsync((int)obj.ParentId);
                if (parent == null)
                {
                    errors.Add("Mã thư mục cha không chính xác.");
                    return HomeDoctorResponse.BAD_REQUEST(errors);
                }
            }
            string newFolderName = obj.Name.GetValidFolderName();
            string newFolderPath = "";
            if (parent != null)
            {
                newFolderPath = Path.Combine(rootFolderPath + parent.Path.Replace("/", @"\"), newFolderName);
            }
            else
            {
                newFolderPath = Path.Combine(rootFolderPath, newFolderName);
            }
            if (!Directory.Exists(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }
            FolderUpload model = new()
            {
                Active = 1,
                CreatedTime = DateTime.Now,
                Name = Path.GetFileName(newFolderPath),
                ParentId = obj.ParentId,
                Path = newFolderPath.Replace(rootFolderPath, "").Replace(@"\", "/"),
                TreeIds = parent != null ? $"{parent.TreeIds}_{parent.Id}" : ""
            };
            await _folderUploadRepository.CreateAsync(model);
            await _folderUploadRepository.SaveChangesAsync();
            return HomeDoctorResponse.SUCCESS(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Get list all folder upload
        /// </summary>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> GetAllFolders()
        {
            var data = await _folderUploadRepository.GetAllAsync();
            foreach (var item in data)
            {
                item.TreeIds = $"{item.TreeIds}_{item.Id}";
            }
            data.OrderByDescending(c => c.TreeIds);
            return HomeDoctorResponse.SUCCESS(data);
        }
        /// <summary>
        /// Auhthor: TUNGTD
        /// Created: 31/07/2023
        /// Description: Save many file
        /// </summary>
        /// <param name="obj">insert upload file DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">not found folder root</exception>
        public async Task<HomeDoctorResponse> SaveFile(InsertUploadFileDTO obj)
        {
            var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
            if (folder == null)
            {
                throw new Exception("Folder not found");
            }
            var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
            var models = await _fileStorageService.SaveManyFile(obj, rootFolder);
            await _uploadFileRepository.CreateListAsync(models);
            await _uploadFileRepository.SaveChangesAsync();
            return HomeDoctorResponse.SUCCESS(models);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: save large file
        /// </summary>
        /// <param name="obj">insert large upload file DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">folder not found</exception>
        public async Task<HomeDoctorResponse> SaveLargeFile(InsertLargeUploadFileDTO obj)
        {
            var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
            if (folder == null)
            {
                throw new Exception("Folder not found");
            }
            var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
            obj.FolderRoot = rootFolder;
            var models = await _fileStorageService.SaveLargeFile(obj, rootFolder);
            await _uploadFileRepository.CreateListAsync(models);
            await _uploadFileRepository.SaveChangesAsync();
            return HomeDoctorResponse.SUCCESS(models);
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Get root web folder in JOBI storage
        /// </summary>
        /// <returns></returns>
        private string GetRootFolderPath()
        {
            string rootPath = _webHostEnvironment.ContentRootPath;
            rootPath = rootPath.Replace(FolderUploadConst.ROOT_API_NAME, FolderUploadConst.ROOT_STORAGE_NAME);
            if (!rootPath.Contains(FolderUploadConst.ROOT_WEB_NAME))
            {
                rootPath = Path.Combine(rootPath, FolderUploadConst.ROOT_WEB_NAME);
            }
            return rootPath;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: get list paging upload file
        /// </summary>
        /// <param name="parameters">paging upload file parameters</param>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> ListPagingFile(PagingUploadFileParameter parameters)
        {
            var data = await _uploadFileRepository.ListPaging(parameters);
            return HomeDoctorResponse.SUCCESS(data);
        }
        public async Task<HomeDoctorResponse> ListPagingFileByAccountId(PagingUploadFileParameter parameters, int accountId)
        {
            var data = await _uploadFileRepository.ListPagingByAccountId(parameters, accountId);
            return HomeDoctorResponse.SUCCESS(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 01/08/2023
        /// Description: Current usre upload file
        /// </summary>
        /// <param name="obj">insert upload file DTO</param>
        /// <param name="user">type user upload</param>
        /// <param name="type">type file upload</param>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> CurrentUserSaveFile(InsertUploadFileDTO obj, string user, string type)
        {

            if (obj.Files.Any(c => c.Length > UploadFileConst.MAXIMUM_UPLOAD_SIZE))
            {
                return HomeDoctorResponse.BAD_REQUEST("Dung lượng tối đa của tệp tin tải lên là 5MB");
            }
            var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
            if (folder == null)
            {
                throw new Exception("Folder not found");
            }
            var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
            List<UploadFiles> data = new();
            switch (type)
            {
                case FolderUploadConst.AVATAR_TYPE:
                    data = await _fileStorageService.SaveAvartar(obj, rootFolder);
                    break;
                case FolderUploadConst.COVER_PHOTO_TYPE:
                    data = await _fileStorageService.SaveCoverPhoto(obj, rootFolder);
                    break;
                default:
                    data = await _fileStorageService.SaveManyFile(obj, rootFolder);
                    break;
            }
            await _uploadFileRepository.CreateListAsync(data);
            await _uploadFileRepository.SaveChangesAsync();
            return HomeDoctorResponse.SUCCESS(data);
        }

		public async Task<HomeDoctorResponse> DeleteFile(DeleteFileDTO obj)
		{
            try
            {
				#region Xóa file trong root
				string rootPath = _webHostEnvironment.ContentRootPath;
                var ids = await _fileStorageService.DeleteFile(obj, rootPath);
				#endregion
				#region Xóa file trong DB
				await _uploadFileRepository.SoftDeleteListAsync(ids);
				await _uploadFileRepository.SaveChangesAsync();
				#endregion
				return HomeDoctorResponse.SUCCESS();
			}
			catch (Exception e)
            {
				return HomeDoctorResponse.BAD_REQUEST();
			}
		}
        public async Task<HomeDoctorResponse> GetAllFoldersByAccountId(int accountId)
        {
            var data = await _folderUploadRepository.GetAllAsync();
            foreach (var item in data)
            {
                item.TreeIds = $"{item.TreeIds}_{item.Id}";
            }
            data.OrderByDescending(c => c.TreeIds);

            //Kiểm tra data đã có folder của account Đăng nhập chưa
            var folderAccount = data.Where(x => x.Name.Contains(accountId.ToString())).ToList();
            if (folderAccount.Count == 0)
            {
                var folderAccountObj = new InsertFolderUploadDTO();
                folderAccountObj.Name = accountId.ToString();
                folderAccountObj.ParentId = 1000019;
                await AddFolder(folderAccountObj);
            }
            data = data.Where(x => x.Id == 1000019 || x.Name.Contains(accountId.ToString())).ToList();
            return HomeDoctorResponse.SUCCESS(data);
        }

        public async Task<List<UploadFiles>> SaveFile2(InsertUploadFileDTO obj)
        {
            var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
            if (folder == null)
            {
                throw new Exception("Folder not found");
            }
            var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
            var models = await _fileStorageService.SaveManyFile(obj, rootFolder);
            await _uploadFileRepository.CreateListAsync(models);
            await _uploadFileRepository.SaveChangesAsync();
            return models;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: save large file
        /// </summary>
        /// <param name="obj">insert large upload file DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">folder not found</exception>
        public async Task<List<UploadFiles>> SaveLargeFile2(InsertLargeUploadFileDTO obj)
        {
            var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
            if (folder == null)
            {
                throw new Exception("Folder not found");
            }
            var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
            obj.FolderRoot = rootFolder;
            var models = await _fileStorageService.SaveLargeFile(obj, rootFolder);
            await _uploadFileRepository.CreateListAsync(models);
            await _uploadFileRepository.SaveChangesAsync();
            return models;
        }
    }
}
