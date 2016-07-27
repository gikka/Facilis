using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace Facilis.Infra.CrossCutting.Identity.Configuration
{
    public class UsuarioConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public UsuarioConfiguration()
        {
            HasKey(u => u.Id);
            Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.Sobrenome)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.DataNascimento)
                .IsRequired();

            Property(u => u.Telefone)
                .IsRequired();

            Property(u => u.Sexo)
                .IsRequired()
                .HasMaxLength(1);

            Property(u => u.Endereco)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.Numero)
                .IsRequired()
                .HasMaxLength(10);

            Property(u => u.Cep)
                .IsRequired();

            Property(u => u.Complemento)
                .HasMaxLength(50);

            Property(u => u.Bairro)
                .IsRequired()
                .HasMaxLength(50);

            //HasRequired(u => u.Cidade)
            //    .WithMany()
            //    .HasForeignKey(u => u.CidadeId);

        }
    }
}
