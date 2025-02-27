using HomeDoctorSolution.Util.Entities;
using System.Text.Json.Serialization;

namespace HomeDoctorSolution.Models
{
    public partial class UploadFiles : EntityCommon<int>
    {
        /// <summary>
        /// Mã FolderUpload
        /// </summary>
        public int FolderUploadId { get; set; }
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Kích thước tệp tin
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Đuôi tệp tin
        /// </summary>
        public string Extension { get; set; } = null!;
        /// <summary>
        /// Đường dẫn tệp
        /// </summary>
        public string Path { get; set; } = null!;
        /// <summary>
        /// Đường dẫn ảnh bìa
        /// </summary>
        public string? ThumbnailPath { get; set; }
        /// <summary>
        /// Loại tệp tin
        /// </summary>
        public string MimeType { get; set; } = null!;
        [JsonIgnore]
        public virtual FolderUpload FolderUpload { get; set; } = null!;
        [JsonIgnore]
        public virtual Account Account { get; set; } = null!;
    }
}
