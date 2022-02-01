using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TIENDA_DE_COCINAS.datos
{
    public class Conexion : IdentityDbContext<IdentityUser>
    {//modificsr tbm
        public Conexion(DbContextOptions<Conexion> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);


            var hasher = new PasswordHasher<IdentityUser>();
            string rol1 = Guid.NewGuid().ToString();
            string rol2 = Guid.NewGuid().ToString();
            string usuario1 = Guid.NewGuid().ToString();
            string usuario2 = Guid.NewGuid().ToString();

            modelbuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = rol1,
                    Name = "VISITANTE",
                    NormalizedName = "VISITANTE",
                    ConcurrencyStamp = rol1.ToString()
                },
                new IdentityRole
                {
                    Id = rol2,
                    Name = "ADMINISTRADOR",
                    NormalizedName = "ADMINISTRADOR",
                    ConcurrencyStamp = rol2.ToString()
                });

            modelbuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = usuario1,
                    UserName = "usuario1@prueba.com",
                    NormalizedUserName = "USUARIO1@PRUEBA.COM",
                    Email = "usuario1@prueba.com",
                    NormalizedEmail = "USUARIO1@PRUEBA.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "1234567890aA*")
                },
                new IdentityUser
                {
                    Id = usuario2,
                    UserName = "usuario2@prueba.com",
                    NormalizedUserName = "USUARIO2@PRUEBA.COM",
                    Email = "usuario2@prueba.com",
                    NormalizedEmail = "USUARIO2@PRUEBA.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "1234567890aA*")
                });

            modelbuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = rol1,
                    UserId = usuario1
                },
                new IdentityUserRole<string>
                {
                    RoleId = rol2,
                    UserId = usuario2
                }
                );

        }

    }
}
