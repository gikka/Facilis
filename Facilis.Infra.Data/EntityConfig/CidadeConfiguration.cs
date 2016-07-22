using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.Repositories.EntityConfig
{
    public class CidadeConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.EstadoId);
        }
    }
}
