using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.Certificate
{
    public class CreateViewModel
    {   [Required]
        [Display(Name ="DateOfIssue")]
        public DateTime DateOfIssue { get; set; }

        [Required]
        [Display(Name ="ValidMonths")]
        public int ValidMonths { get; set; }
    }
}
