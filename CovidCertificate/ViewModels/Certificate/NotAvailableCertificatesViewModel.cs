using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.Certificate
{
    public class NotAvailableCertificatesViewModel
    {
        public int Count { get; set; }
        public ICollection<CertificateViewModel> Certificates { get; set; }
    }
}
