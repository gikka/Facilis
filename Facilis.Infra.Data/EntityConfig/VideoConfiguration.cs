using Facilis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilis.Infra.Data.EntityConfig
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            HasKey(v => v.VideoId);

            Property(v => v.URL)
                .IsRequired()
                .HasMaxLength(1000);

            HasRequired(v => v.Evento)
                .WithMany()
                .HasForeignKey(v => v.EventoId);
        }
    }
}
