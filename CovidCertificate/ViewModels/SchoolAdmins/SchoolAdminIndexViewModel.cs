using CovidCertificate.ViewModels.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.SchoolAdmins
{
    public class SchoolAdminIndexViewModel
    {
        public int TotalTestsNextWeek { get; set; }
        public ICollection<UserViewModel> Teachers { get; set; } = new List<UserViewModel>();
        public ICollection<UserViewModel> Students { get; set; } = new List<UserViewModel>();
    }
}
