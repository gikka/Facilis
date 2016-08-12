using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.EntityConfig
{
    public class ParticipanteConfiguration : EntityTypeConfiguration<Participante>
    {
        public ParticipanteConfiguration()
        {
            HasKey(p => p.ParticipanteId);

            Property(p => p.DataInscricao)
                .IsRequired();

            HasRequired(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);

            Property(p => p.UsuarioId)
                .HasMaxLength(128);
            
            HasRequired(p => p.Evento)
                .WithMany()
                .HasForeignKey(p => p.EventoId);

            HasOptional(p => p.Cupom)
                .WithMany()
                .HasForeignKey(p => p.CupomId);
        }
    }
}
