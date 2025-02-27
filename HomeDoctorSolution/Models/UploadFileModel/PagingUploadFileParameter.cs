namespace HomeDoctorSolution.Models.UploadFileModel
{
    public class PagingUploadFileParameter : PagingBaseParameters
    {
        public int FodlderUploadId { get; set; }
        public string ContentType { get; set; } = null!;
    }
}
