namespace HomeDoctorSolution.Models.UploadFileModel
{
    public class PagingBaseParameters
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; } = null!;
        public int PageStart
        {
            get
            {
                return PageIndex * PageSize;
            }
        }
    }
}
