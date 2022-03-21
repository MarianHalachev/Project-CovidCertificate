using System.Threading.Tasks;

namespace CovidCertificate.Data
{
    public interface IDataSeeder
    {
        Task Run();
        Task SeedSchoolAndSchoolAdmins();
        Task SeedSchoolStudents();
        Task SeedSchoolTeachers();
    }
}