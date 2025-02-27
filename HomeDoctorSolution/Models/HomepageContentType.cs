using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class HomepageContentType
    {
        public HomepageContentType()
        {
            HomepageContents = new HashSet<HomepageContent>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<HomepageContent> HomepageContents { get; set; }
    }
}
