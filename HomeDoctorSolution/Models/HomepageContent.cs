using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class HomepageContent
    {
        public HomepageContent()
        {
            HomepageContentMeta = new HashSet<HomepageContentMeta>();
        }

        public int Id { get; set; }
        public int HomepageContentTypeId { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Url { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual HomepageContentType HomepageContentType { get; set; } = null!;
        public virtual ICollection<HomepageContentMeta> HomepageContentMeta { get; set; }
    }
}
