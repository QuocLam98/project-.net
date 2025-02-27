using System.ComponentModel.DataAnnotations;

namespace HomeDoctorSolution.Models.ModelDTO
{
    public class TagForSelect2Aggregates
    {
        [Range(1, Int32.MaxValue)]
        public int PageIndex { get; set; }
        [Range(1, 200)]
        public int PageSize { get; set; }
        [MaxLength(500)]
        public string Keyword { get; set; }

        public string SortType { get; set; }
        public string OrderBy { get; set; }

        public int TagTypeId { get; set; }
    }
}
