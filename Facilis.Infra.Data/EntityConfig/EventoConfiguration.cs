using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.EntityConfig
{
    public class EventoConfiguration : EntityTypeConfiguration<Evento>
    {
        public EventoConfiguration()
        {
            HasKey(e => e.EventoId);

            Property(e => e.DataFinal)
                .IsRequired();

            Property(e => e.DataInicial)
                .IsRequired();

            Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(5000);

            Property(e => e.EmailPalestrante)
                .IsRequired()
                .HasMaxLength(256);

            Property(e => e.HoraFinal)
                .IsRequired();

            Property(e => e.HoraInicial)
                .IsRequired();

            Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(e => e.NomePalestrante)
                .IsRequired()
                .HasMaxLength(200);

            Property(e => e.QuantidadeVagas)
                .IsRequired();

            Property(e => e.TipoEvento)
                .IsRequired();

        }
    }
}
