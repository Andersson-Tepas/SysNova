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
    public class MetodoPagoConfiguration : IEntityTypeConfiguration<MetodoPago>
    {
        public void Configure(EntityTypeBuilder<MetodoPago> builder)
        {
            // Nombre de la tabla
            builder.ToTable("MetodoPago");

            // Llave primaria
            builder.HasKey(x => x.MetodoPagoId);

            // Propiedades
            builder.Property(x => x.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Descripcion)
                   .HasMaxLength(500);

            // Índices
            builder.HasIndex(x => x.Nombre)
                   .IsUnique();
        }
    }
}