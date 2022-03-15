using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.SchoolAdmins
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public bool HasValidCertificate { get; set; }
    }
}
