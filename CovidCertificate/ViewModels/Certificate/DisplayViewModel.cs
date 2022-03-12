﻿using System;
using CovidCertificate.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.ViewModels.Certificate
{
    public class DisplayViewModel
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string ValidMonths { get; set; }
        public bool IsValid { get; set; }
        public User User { get; set; }
    }
}
