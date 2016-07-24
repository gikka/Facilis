using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facilis.Infra.Data.Repositories.EntityConfig
{
    public class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration()
        {
            HasKey(e => e.EstadoId);

            Property(e => e.EstadoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                
            Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Sigla)
                .IsRequired()
                .HasMaxLength(2);

        }
    }
}
