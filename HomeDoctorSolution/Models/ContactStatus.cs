using System;
using System.Collections.Generic;

namespace HomeDoctorSolution.Models
{
    public partial class ContactStatus
    {
        public ContactStatus()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
