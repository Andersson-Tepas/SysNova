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
    public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
    {
        public void Configure(EntityTypeBuilder<Contacto> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Contacto");

            // Llave primaria
            builder.HasKey(x => x.ContactoId);

            // Propiedades
            builder.Property(x => x.NombreCompleto)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Correo)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Telefono)
                   .HasMaxLength(20);

            builder.Property(x => x.Asunto)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Mensaje)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.Leido)
                   .IsRequired();

            builder.Property(x => x.FechaContacto)
                   .IsRequired();

            // Índices
            builder.HasIndex(x => x.Correo);

            builder.HasIndex(x => x.FechaContacto);

            builder.HasIndex(x => x.Leido);
        }
    }
} 
