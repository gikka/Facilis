using Facilis.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.EntityConfig
{
    public class CidadeConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfiguration()
        {
            HasKey(c => c.CidadeId);

            Property(c => c.CidadeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.EstadoId);

 
        }
    }
}
