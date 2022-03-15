﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CovidCertificate.Common
{
    public static class SeedRoles
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole<string>>>();
            var adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;
            var userRoleExists = roleManager.RoleExistsAsync("User").Result;
            if (!adminRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole<string>("Admin"));
            }

            if (!userRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole<string>("User"));
            }
        }
    }
}
