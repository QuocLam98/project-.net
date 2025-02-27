using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Models.UploadFileModel;
using HomeDoctorSolution.Services.UploadFile.Interfaces;
using HomeDoctorSolution.Util;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace HomeDoctorSolution.Services.UploadFile
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: add folder
        /// </summary>
        /// <param name="folderPath">folder full path</param>
        /// <returns></returns>
        public string AddFolder(string folderPath)
        {
            int count = 1;
            string folderName = Path.GetFileNameWithoutExtension(folderPath);
            string parentDirectory = Path.GetDirectoryName(folderPath);
            string newFolderName = folderName;
            while (!Directory.Exists(folderPath))
            {
                newFolderName = string.Format("{0}({1})", folderName, count++);
                folderPath = Path.Combine(parentDirectory, newFolderName);
            }
            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        public Task<UploadFiles> SaveFile(IFormFile file, string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<UploadFiles> SaveImage(IFormFile file, string filePath)
        {
            using (var image = Image.Load(file.OpenReadStream()))
            {
                if (image.Width > UploadFileConst.MAXIMUM_IMAGE_WIDTH)//resize file img
                {
                    float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUM_IMAGE_WIDTH);
                    int newHeight = (int)MathF.Ceiling(image.Height / scale);
                    image.Mutate(h => h.Resize(UploadFileConst.MAXIMUM_IMAGE_WIDTH, newHeight));
                }
                await image.SaveAsync(filePath);
                string thumbPath = await CreateThumbnail(filePath, image);
                return new UploadFiles()
                {
                    ThumbnailPath = thumbPath.Replace(GetRootFolderPath(), "").GetWebFormatPath()
                };
            }
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 30//0
        /// Description: Created thumbnail
        /// </summary>
        /// <param name="filePath">file image full path</param>
        /// <param name="image">image</param>
        public async Task<string> CreateThumbnail(string filePath, Image image)//funciton only run in using stream or file
        {
            string rootPath = Path.Combine(Path.GetDirectoryName(filePath), FolderUploadConst.FOLDER_THUMB_NAME);
            string fileName = Path.GetFileName(filePath);
            var newFullPath = Path.Combine(rootPath, fileName);
            string newThumbName = fileName;
            string newThumnailsPath = newFullPath;
            Image thumb = image;
            int count = 1;
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            if (thumb.Width > UploadFileConst.MAXIMUN_THUMB_WIDTH)
            {
                float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUN_THUMB_WIDTH);
                int newHeight = (int)MathF.Ceiling(thumb.Height / scale);
                thumb.Mutate(h => h.Resize(UploadFileConst.MAXIMUN_THUMB_WIDTH, newHeight));
            }
            while (File.Exists(newFullPath))
            {
                newThumbName = string.Format("{0}({1})", newThumbName, count++);
                newThumnailsPath = Path.Combine(rootPath, newThumbName);
            }
            await thumb.SaveAsync(newThumnailsPath);
            return newThumnailsPath;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: save larger file 
        /// </summary>
        /// <param name="obj">inset large upload file DTO object</param>
        /// <param name="rootFolder">folder root path</param>
        /// <returns></returns>
        /// <exception cref="Exception">stream null exception</exception>
        public async Task<List<UploadFiles>> SaveLargeFile(InsertLargeUploadFileDTO obj, string rootFolder)
        {
            List<UploadFiles> result = new();
            string uploadFolder = obj.FolderRoot;
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var boundary = GetBoundary(MediaTypeHeaderValue.Parse(obj.ContentType));
            if (obj.Stream == null)
            {
                throw new Exception("Stream not null");
            }
            var multipartReader = new MultipartReader(boundary, obj.Stream);
            var section = await multipartReader.ReadNextSectionAsync();
            while (section != null)
            {
                var fileSection = section.AsFileSection();
                if (fileSection != null)
                {
                    var path = Path.Combine(uploadFolder, fileSection.FileName);
                    int count = 1;
                    string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                    string extension = Path.GetExtension(path);
                    string newFullPath = path;
                    string newFileName = fileNameOnly;
                    while (File.Exists(newFullPath))
                    {
                        newFileName = string.Format("{0}({1})", fileNameOnly, count++);
                        newFullPath = Path.Combine(uploadFolder, newFileName + extension);
                    }
                    await SaveLagreFileAsync(fileSection, newFullPath);
                    var contentType = fileSection.Section.ContentType;
                    FileInfo fileInfo = new FileInfo(newFullPath);
                    var file = new UploadFiles()
                    {
                        Size = fileInfo.Length,
                        FolderUploadId = obj.FolderUploadId,
                        AccountId = obj.AdminUploadId,
                        Active = 1,
                        CreatedTime = DateTime.Now,
                        Extension = fileInfo.Extension,
                        Name = fileInfo.Name,
                        Path = newFullPath.Replace(_webHostEnvironment.WebRootPath, "").Replace(@"\", "/"),
                        MimeType = contentType,
                        ThumbnailPath = UploadFileConst.DEFAULT_THUMB
                    };
                    result.Add(file);
                }
                section = await multipartReader.ReadNextSectionAsync();
            }
            return result.ToList();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 30/07/2023
        /// Description: Upload many file 
        /// </summary>
        /// <param name="obj">insert upload file DTO</param>
        /// <param name="rootPath">folder upload full path</param>
        /// <returns></returns>
        public async Task<List<UploadFiles>> SaveManyFile(InsertUploadFileDTO obj, string rootPath)
        {
            List<UploadFiles> result = new();
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
            }
            foreach (var item in obj.Files)
            {
                var path = Path.Combine(rootPath, item.FileName);
                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                string newFullPath = path;
                string newFileName = fileNameOnly;
                string thumbPath = "";
                while (File.Exists(newFullPath))
                {
                    newFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    newFullPath = Path.Combine(rootPath, newFileName + extension);
                }
                if (UploadFileConst.FILE_IMG_EXTENSION.Contains(extension))
                {
                    thumbPath = (await SaveImage(item, newFullPath)).ThumbnailPath;
                }
                else
                {
                    using (var stream = new FileStream(newFullPath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                }
                FileInfo fileInfo = new FileInfo(newFullPath);
                var file = new UploadFiles()
                {
                    Size = fileInfo.Length,
                    FolderUploadId = obj.FolderUploadId,
                    AccountId = obj.AdminAccountId == 0 ? AdminAccountConst.SUPER_ADMIN_ID : obj.AdminAccountId,
                    Active = 1,
                    CreatedTime = DateTime.Now,
                    Extension = fileInfo.Extension,
                    Name = fileInfo.Name,
                    Path = newFullPath.Replace(GetRootFolderPath(), "").Replace(@"\", "/"),
                    MimeType = item.ContentType,
                    ThumbnailPath = string.IsNullOrEmpty(thumbPath) ? UploadFileConst.DEFAULT_THUMB : thumbPath
                };
                result.Add(file);
            }
            return result;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 12/06/2023
        /// Description: Get boundary of file
        /// </summary>
        /// <param name="contentType">content tyoe</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        /// <summary>
        /// Author: TUNGTD
        /// </summary>
        /// <param name="fileSection"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<long> SaveLagreFileAsync(FileMultipartSection fileSection, string filePath)
        {
            await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024);
            await fileSection.FileStream?.CopyToAsync(stream);
            return fileSection.FileStream.Length;
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
        /// Created: 01/08/2023
        /// Description: Save cover photo
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public async Task<List<UploadFiles>> SaveCoverPhoto(InsertUploadFileDTO obj, string rootPath)
        {
            List<UploadFiles> result = new();
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
            }
            foreach (var item in obj.Files)
            {
                var path = Path.Combine(rootPath, item.FileName);
                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileNameOnly;
                string newFullPath = Path.Combine(rootPath, newFileName + extension);
                while (File.Exists(newFullPath))
                {
                    newFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    newFullPath = Path.Combine(rootPath, newFileName + extension);
                }
                using (var image = Image.Load(item.OpenReadStream()))
                {
                    if (image.Width > UploadFileConst.MAXIMUM_COVER_PHOTO_WIDTH)//resize file img
                    {
                        float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUM_IMAGE_WIDTH);
                        int newHeight = (int)MathF.Ceiling(image.Height / scale);
                        image.Mutate(h => h.Resize(UploadFileConst.MAXIMUM_IMAGE_WIDTH, newHeight));
                    }
                    await image.SaveAsync(newFullPath);
                }
                FileInfo fileInfo = new FileInfo(newFullPath);
                var file = new UploadFiles()
                {
                    Size = fileInfo.Length,
                    FolderUploadId = obj.FolderUploadId,
                    AccountId = obj.AdminAccountId == 0 ? AdminAccountConst.SUPER_ADMIN_ID : obj.AdminAccountId,
                    Active = 1,
                    CreatedTime = DateTime.Now,
                    Extension = fileInfo.Extension,
                    Name = fileInfo.Name,
                    Path = newFullPath.Replace(GetRootFolderPath(), "").Replace(@"\", "/"),
                    MimeType = item.ContentType,
                    ThumbnailPath = UploadFileConst.DEFAULT_THUMB
                };
                result.Add(file);
            }
            return result;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 01/08/2023
        /// Description: 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public async Task<List<UploadFiles>> SaveAvartar(InsertUploadFileDTO obj, string rootPath)
        {
            List<UploadFiles> result = new();
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
            }
            foreach (var item in obj.Files)
            {
                var path = Path.Combine(rootPath, item.FileName);
                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileNameOnly;
                string newFullPath = Path.Combine(rootPath, newFileName + extension);
                while (File.Exists(newFullPath))
                {
                    newFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    newFullPath = Path.Combine(rootPath, newFileName + extension);
                }
                using (var image = Image.Load(item.OpenReadStream()))
                {
                    if (image.Width > UploadFileConst.MAXIMUM_AVARTAR_WIDTH)//resize file img
                    {
                        float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUM_IMAGE_WIDTH);
                        int newHeight = (int)MathF.Ceiling(image.Height / scale);
                        image.Mutate(h => h.Resize(UploadFileConst.MAXIMUM_IMAGE_WIDTH, newHeight));
                    }
                    await image.SaveAsync(newFullPath);
                }
                FileInfo fileInfo = new FileInfo(newFullPath);
                var file = new UploadFiles()
                {
                    Size = fileInfo.Length,
                    FolderUploadId = obj.FolderUploadId,
                    AccountId = obj.AdminAccountId == 0 ? AdminAccountConst.SUPER_ADMIN_ID : obj.AdminAccountId,
                    Active = 1,
                    CreatedTime = DateTime.Now,
                    Extension = fileInfo.Extension,
                    Name = fileInfo.Name,
                    Path = newFullPath.Replace(GetRootFolderPath(), "").Replace(@"\", "/"),
                    MimeType = item.ContentType,
                    ThumbnailPath = UploadFileConst.DEFAULT_THUMB
                };
                result.Add(file);
            }
            return result;
        }

        public string GetBoundary(MediaTypeHeaderValue contentType)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

            if (string.IsNullOrWhiteSpace(boundary))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }
            return boundary;
        }

		public async Task<List<int>> DeleteFile(DeleteFileDTO obj, string rootPath)
		{
			if (!Directory.Exists(rootPath))
			{
				Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
			}
			foreach (var path in obj.ListPaths)
			{
				var fullPath = rootPath + path.Path;
				var fullThumbnailPath = rootPath + path.ThumbnailPath;
				FileInfo filePath = new FileInfo(fullPath);
				FileInfo fileThumbPath = new FileInfo(fullThumbnailPath);
				// Check if the file exists.
				if (filePath.Exists || fileThumbPath.Exists)
				{
					// Delete the file.
					filePath.Delete();
					fileThumbPath.Delete();
				}
			}
			List<int> ids = new List<int>();
			ids.AddRange(obj.ListPaths.Select(x => x.Id).ToList());
			return ids;
		}
	}
}
