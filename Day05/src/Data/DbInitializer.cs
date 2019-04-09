using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Day05.Data
{
    public static class DbInitializer
    {
        public static void Seed( ApplicationDbContext context)
        {
            context.Database.Migrate();
            if(!context.Roles.Any())
            {
                var adminRole = new ApplicationRole { Name = "Administrador", NormalizedName = "ADMINISTRADOR" };
                var ventasRole = new ApplicationRole { Name = "Ventas", NormalizedName = "VENTAS" };
                var userRole = new ApplicationRole { Name = "Usuarios", NormalizedName = "USUARIOS" };

                context.Roles.Add(adminRole);
                context.Roles.Add(ventasRole);
                context.Roles.Add(userRole);
                context.SaveChanges();

                // Add admin user
                var adminUser = new ApplicationUser
                {
                    Email = "admin@megsoftconsulting.com",
                    NormalizedEmail = "ADMIN@MEGSOFTCONSULTING.COM",
                    UserName = "admin@megsoftconsulting.com",
                    NormalizedUserName = "ADMIN@MEGSOFTCONSULTING.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEMJuQFPIpHk7F4cztOrbbr/eoASulqGI4MjLvd7BCIgeRNI1J1zPk4dV3STg92e1Ng==",
                    SecurityStamp = "SSF2QD5C5SURX3BD2C5XWNR3VBUFUGC7",
                };
                context.Users.Add(adminUser);
                context.SaveChanges();
                context.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>() { RoleId = adminRole.Id, UserId = adminUser.Id });
                context.SaveChanges();
            }
        }


    }
}
