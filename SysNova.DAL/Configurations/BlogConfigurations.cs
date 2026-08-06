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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Blog");

            // Llave primaria
            builder.HasKey(x => x.BlogId);

            // Propiedades
            builder.Property(x => x.Titulo)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Resumen)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.Contenido)
                   .HasMaxLength(5000)
                   .IsRequired();

            builder.Property(x => x.Imagen)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.Autor)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.FechaPublicacion)
                   .IsRequired();

            builder.Property(x => x.Visitas)
                   .IsRequired();

            // Índices
            builder.HasIndex(x => x.FechaPublicacion);

            builder.HasIndex(x => x.Autor);

            builder.HasIndex(x => x.Visitas);
        }
    }
}
