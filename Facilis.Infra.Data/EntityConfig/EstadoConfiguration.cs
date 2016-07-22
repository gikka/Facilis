using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.Repositories.EntityConfig
{
    public class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Sigla)
                .IsRequired()
                .HasMaxLength(2);
        }
    }
}
