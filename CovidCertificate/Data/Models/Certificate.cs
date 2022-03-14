using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.Data.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime EndDate { get; set; }
        public int ValidMonths { get; set; }
        public virtual User User { get; set; }
    }
}
