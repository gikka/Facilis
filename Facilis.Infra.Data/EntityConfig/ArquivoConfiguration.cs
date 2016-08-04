using Facilis.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.Data.EntityConfig
{
    public class ArquivoConfiguration : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfiguration()
        {
            HasKey(a => a.ArquivoId);

            Property(a => a.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.Descricao)
                .IsRequired()
                .HasMaxLength(250);

            Property(a => a.ContentType)
                .IsRequired()
                .HasMaxLength(200);

            Property(a => a.ContentType)
                .IsRequired();

            HasRequired(a => a.Evento)
                .WithMany()
                .HasForeignKey(a => a.EventoId);
        }
    }
}
