﻿using CovidCertificate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.Certificate
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public int ValidMonths { get; set; }
        public User User { get; set; }
    }
}