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
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Marca");

            // Llave primaria
            builder.HasKey(x => x.MarcaId);

            // Propiedades
            builder.Property(x => x.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Descripcion)
                   .HasMaxLength(500);

            builder.Property(x => x.Logo)
                   .HasMaxLength(300);

            builder.Property(x => x.Pais)
                   .HasMaxLength(100);

            // Índices
            builder.HasIndex(x => x.Nombre)
                   .IsUnique();

            builder.HasIndex(x => x.Pais);
        }
    }
}