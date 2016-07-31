using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Facilis.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IDisposable
    {
        public ApplicationDbContext()
         : base("Facilis", throwIfV1Schema: false)
        {
        }

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Properties()
            //    .Where(p => p.Name == "Id")
            //    .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            //modelBuilder.Configurations.Add(new Configuration.UsuarioConfiguration());

            //modelBuilder.Entity<IdentityUser>()
            //    .ToTable("Usuario")
            //    .Property(p => p.Id)
            //    .HasColumnName("Id");

            //modelBuilder.Entity<ApplicationUser>()
            //    .ToTable("Usuario")
            //    .Property(p => p.Id)
            //    .HasColumnName("Id");

            //modelBuilder.Entity<IdentityUserRole>()
            //    .ToTable("UsuariosRole");

            //modelBuilder.Entity<IdentityUserLogin>()
            //    .ToTable("Logins");

            //modelBuilder.Entity<IdentityUserClaim>()
            //    .ToTable("Claims");

            //modelBuilder.Entity<IdentityRole>()
            //    .ToTable("Roles");

        }
    }
}
