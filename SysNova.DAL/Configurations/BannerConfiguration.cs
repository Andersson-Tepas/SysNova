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
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Banner");

            // Llave primaria
            builder.HasKey(x => x.BannerId);

            // Propiedades
            builder.Property(x => x.Titulo)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.SubTitulo)
                   .HasMaxLength(300);

            builder.Property(x => x.Imagen)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.BotonTexto)
                   .HasMaxLength(100);

            builder.Property(x => x.BotonUrl)
                   .HasMaxLength(300);

            builder.Property(x => x.Orden)
                   .IsRequired();

            builder.Property(x => x.Mostrar)
                   .IsRequired();

            // Índices
            builder.HasIndex(x => x.Orden);

            builder.HasIndex(x => x.Mostrar);
        }
    }
}