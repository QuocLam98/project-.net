namespace HomeDoctorSolution.Models.UploadFileModel
{
    public class InsertLargeUploadFileDTO
    {
        /// <summary>
        /// mã thư mục upload
        /// </summary>
        public int FolderUploadId { get; set; }
        /// <summary>
        /// Mã quản trị viên upload tệp tin
        /// </summary>
        public int AdminUploadId { get; set; }

        /// <summary>
        /// Nguồn của tệp tin
        /// </summary>
        public Stream Stream { get; set; } = null!;
        /// <summary>
        /// Đường dẫn của tệp tin
        /// </summary>
		public string FolderRoot { get; set; } = null!;
        public string ContentType { get; set; } = null!;
    }
}
