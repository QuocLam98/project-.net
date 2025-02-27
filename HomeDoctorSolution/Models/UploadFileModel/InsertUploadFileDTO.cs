namespace HomeDoctorSolution.Models.UploadFileModel
{
    public class InsertUploadFileDTO
    {
        /// <summary>
        /// Mã FolderUpload
        /// </summary>
        public int FolderUploadId { get; set; }
        /// <summary>
        /// Mã tài khoản admin
        /// </summary>
        public int AdminAccountId { get; set; }
        /// <summary>
        /// Tệp tin đẩy lên
        /// </summary>
        public ICollection<IFormFile> Files { get; set; } = default!;
    }
}
