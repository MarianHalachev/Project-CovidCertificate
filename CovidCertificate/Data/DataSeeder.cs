using CovidCertificate.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.Data
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public DataSeeder(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<string>> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task Run()
        {
          await  SeedSchoolAndSchoolAdmins();
          await  SeedSchoolStudents();
        }

        public async Task SeedSchoolStudents()
        {
            if (userManager.GetUsersInRoleAsync("student").Result.Count() >= 50)
            {
                return;
            }

            School vl = context.School.FirstOrDefault(x => x.CodeByAdmin == "1");
            School hb = context.School.FirstOrDefault(x => x.CodeByAdmin == "2");

            for (int i = 0; i < 50; i++)
            {
                try
                {
                    Random random = new Random();
                    ICollection<Certificate> certificatesVl = new HashSet<Certificate>();
                    ICollection<Certificate> certificatesHb = new HashSet<Certificate>();
                    for (int j = 0; j < 5; j++)
                    {
                        certificatesVl.Add(new Certificate()
                        {
                            DateOfIssue = DateTime.Now.AddMonths(random.Next(-10, 10)),
                            ValidMonths = 3
                        }
                        );
                        certificatesHb.Add(new Certificate()
                        {
                            DateOfIssue = DateTime.Now.AddMonths(random.Next(-10, 10)),
                            ValidMonths = 3
                        }
                        );
                    }

                    User vlStudent = new User()
                    {
                        UserName = $"vlstudent{i}",
                        Email = $"vlstudent{i}@vl.bg",
                        FirstName = $"StudentVl{i}",
                        MiddleName = $"StudentVl{i}",
                        LastName = $"StudentVl{i}",
                        SchoolId = vl.Id,
                        Certificate = certificatesVl
                    };
                    var resultVl = this.userManager.CreateAsync(vlStudent, "admin").GetAwaiter().GetResult();
                    var roleResult1 = this.userManager.AddToRoleAsync(vlStudent, "Student").GetAwaiter().GetResult(); 
                    User hbStudent = new User()
                    {
                        UserName = $"hbstudent{i}",
                        Email = $"hbstudent{i}@hb.bg",
                        FirstName = $"StudentHb{i}",
                        MiddleName = $"StudentHb{i}",
                        LastName = $"StudentHb{i}",
                        SchoolId = hb.Id,
                        Certificate = certificatesHb
                    };
                  
                    var resultHb = this.userManager.CreateAsync(hbStudent, "admin").GetAwaiter().GetResult(); 
                   
                    var roleResult2 = this.userManager.AddToRoleAsync(hbStudent, "Student").GetAwaiter().GetResult(); 
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


        public async Task SeedSchoolAndSchoolAdmins()
        {
            if (context.School.Any())
            {
                return;
            }

            School vl = new School()
            {
                Name = "Vasil Levski",
                Address = "Velingrad",
                CodeByAdmin = "1",
                IsConfirmed = true,
            };
            School hb = new School()
            {
                Name = "Hristo Botev",
                Address = "Velingrad",
                CodeByAdmin = "2",
                IsConfirmed = true,
            };
            context.School.AddRange(vl, hb);
            await context.SaveChangesAsync();

            var userVl = new User
            {
                UserName = "adminvl",
                Email = "vl@abv.bg",
                FirstName = "AdminVl",
                MiddleName = "AdminVl",
                LastName = "AdminVl",
                School = context.School.FirstOrDefault(x => x.CodeByAdmin == "1")
            };
            var userHb = new User
            {
                UserName = "adminhb",
                Email = "hb@abv.bg",
                FirstName = "AdminHb",
                MiddleName = "AdminHb",
                LastName = "AdminHb",
                School = context.School.FirstOrDefault(x => x.CodeByAdmin == "2")
            };
            var resultVl = await this.userManager.CreateAsync(userVl, "admin");
            var resultHb = await this.userManager.CreateAsync(userHb, "admin");

            if (resultVl.Succeeded && resultHb.Succeeded)
            {
                vl.Admin = userVl;
                hb.Admin = userHb;
                context.School.Update(vl);
                context.School.Update(hb);
                await context.SaveChangesAsync();
                int schoolVlId = context.School.FirstOrDefault(x => x.CodeByAdmin == vl.CodeByAdmin).Id;
                int schoolHbId = context.School.FirstOrDefault(x => x.CodeByAdmin == hb.CodeByAdmin).Id;
                userVl.SchoolId = schoolVlId;
                userHb.SchoolId = schoolHbId;
                context.Users.Update(userVl);
                context.Users.Update(userHb);
                await context.SaveChangesAsync();
                var roleResult1 = await this.userManager.AddToRoleAsync(userVl, "SchoolAdmin");
                var roleResult2 = await this.userManager.AddToRoleAsync(userVl, "SchoolAdmin");

            }
        }

    }
}
