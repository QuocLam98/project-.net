namespace HomeDoctorSolution.Models.UploadFileModel
{
    public class InsertFolderUploadDTO
    {
        /// <summary>
		/// Mã thư mục cha
		/// </summary>
		public int? ParentId { get; set; }
        /// <summary>
        /// Tên thư mục
        /// </summary>
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
