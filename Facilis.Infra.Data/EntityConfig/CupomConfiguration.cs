using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.EntityConfig
{
    public class CupomConfiguration : EntityTypeConfiguration<Cupom>
    {
        public CupomConfiguration()
        {
            HasKey(c => c.CupomId);
            
            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.PercentualDesconto)
                .IsRequired();

            Property(c => c.Quantidade)
                .IsRequired();

            Property(c => c.Validade)
                .IsRequired();

            HasRequired(c => c.Evento)
                .WithMany()
                .HasForeignKey(c => c.EventoId);

            Property(c => c.UsuarioId)
                 .HasMaxLength(128);

            HasRequired(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId);

        }
    }
}
