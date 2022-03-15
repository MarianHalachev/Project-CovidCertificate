using CovidCertificate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.Services.Interfaces
{
    public interface ICovidService
    {

        void CreateCertificate(DateTime dateofIssue,int validMonths, string userId);
        List<Certificate> GetAllCertificates();

        Certificate GetCertificateById(int id);

        void EditCertificate(int certificateId,DateTime dateofIssue,int validMonths);

        void DeleteCertificate(int certificateId);

        void CreateSchool (string name,string codebyAdmin,string address);

        School GetSchoolById(int id);

        void EditSchool(int schoolId,string name,string codebyAdmin,string address);

        void DeleteSchool(int schoolId);



    }
}
