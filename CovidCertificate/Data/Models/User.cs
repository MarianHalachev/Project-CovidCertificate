using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? SchoolId { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<Certificate> Certificate { get; set; } = new HashSet<Certificate>();
    }
}
