﻿using System.Data.Entity;
using Facilis.Domain.Entities;
using Facilis.Infra.Data.EntityConfig;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Facilis.Infra.CrossCutting.Identity.Model;

namespace Facilis.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("Facilis")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new UsuarioConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Usuarios")
                .Property(p => p.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Usuarios")
                .Property(p => p.Id)
                .HasColumnName("Id");


        }
    }
}