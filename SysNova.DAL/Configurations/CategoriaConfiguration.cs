using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DAL.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Categoria");

            // Llave primaria
            builder.HasKey(x => x.CategoriaId);

            // Propiedades
            builder.Property(x => x.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Descripcion)
                   .HasMaxLength(500);

            builder.Property(x => x.Icono)
                   .HasMaxLength(200);

            builder.Property(x => x.Imagen)
                   .HasMaxLength(300);

            // Índices
            builder.HasIndex(x => x.Nombre)
                   .IsUnique();
        }
    }
} 