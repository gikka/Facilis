using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Model;
using Facilis.Infra.Data.EntityConfig;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Facilis.Infra.Data.Context
{
    public class FacilisContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public FacilisContext()
            : base("Facilis")
        {

        }

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
              .Where(p => p.Name == p.ReflectedType.Name + "Id")
            .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));


            modelBuilder.Configurations.Add(new CidadeConfiguration());
            modelBuilder.Configurations.Add(new EstadoConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new EventoConfiguration());
            modelBuilder.Configurations.Add(new CupomConfiguration());
            modelBuilder.Configurations.Add(new ArquivoConfiguration());
            modelBuilder.Configurations.Add(new VideoConfiguration());

        }

        public static FacilisContext Create()
        {
            return new FacilisContext();
        }
    }
}
