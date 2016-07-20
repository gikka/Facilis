using Facilis.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Facilis.Infra.Data.Context
{
    public class FacilisContext : DbContext 
    {
        public FacilisContext()
            : base("Facilis")
        {

        }

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

           // modelBuilder.Configurations.Add(new UsuarioConfiguration());
            //modelBuilder.Configurations.Add(new CidadeConfiguration());
            //modelBuilder.Configurations.Add(new EstadoConfiguration());

            modelBuilder.Entity().Map 
        }

    }
}
