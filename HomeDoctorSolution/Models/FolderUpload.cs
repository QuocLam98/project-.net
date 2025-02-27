using HomeDoctorSolution.Util.Entities;

namespace HomeDoctorSolution.Models
{
    public class FolderUpload : EntityCommon<int>
    {
        /// <summary>
        /// Đường dẫn thư mục
        /// </summary>
        public string Path { get; set; } = null!;
        /// <summary>
        /// Mã thư mục cha
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Mã cây thư mục
        /// </summary>
        public string TreeIds { get; set; } = null!;

        public virtual ICollection<UploadFiles> UploadFiles { get; } = new List<UploadFiles>();
    }
}
