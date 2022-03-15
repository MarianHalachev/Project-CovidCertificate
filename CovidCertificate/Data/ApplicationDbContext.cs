namespace CovidCertificate.Data
{
    using CovidCertificate.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CovidCertificate.Data.Models.School> School { get; set; }
        public virtual DbSet<CovidCertificate.Data.Models.Certificate> Certificate { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<School>(option =>
            {
                option.HasOne<User>(s => s.Admin)
                   .WithOne(ad => ad.School)
                   .HasForeignKey<School>(ad => ad.AdminId);
            });

            builder.Entity<IdentityRole<string>>(option =>
            {
                option.HasData(new IdentityRole<string>()
                {
                    Id = "AdminRoleId",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
                option.HasData(new IdentityRole<string>()
                {
                    Id = "SchoolAdminRoleId",
                    Name = "SchoolAdmin",
                    NormalizedName = "SCHOOLADMIN"
                });
                option.HasData(new IdentityRole<string>()
                {
                    Id = "TeacherRoleId",
                    Name = "Teacher",
                    NormalizedName = "Teacher"
                });
                option.HasData(new IdentityRole<string>()
                {
                    Id = "StudentRoleId",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                });
            });
            builder.Entity<User>(option =>
            {

                option.HasData(new User()
                {
                    Id = "adminId",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "admin"),
                    Email = "admin@covid.bg",
                    NormalizedEmail = "admin@covid.bg",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty
                }); ;
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "AdminRoleId",
                UserId = "adminId"
            });
            base.OnModelCreating(builder);
        }


    }
}
