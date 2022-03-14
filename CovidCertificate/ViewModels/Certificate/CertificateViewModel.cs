using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.Certificate
{
    public class CertificateViewModel
    {
        public string FullName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsValid { get; set; }
    }
}
