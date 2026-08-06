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
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Cliente");

            // Llave primaria
            builder.HasKey(x => x.ClienteId);

            // Propiedades
            builder.Property(x => x.Nombres)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Apellidos)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Correo)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Password)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(x => x.Telefono)
                   .HasMaxLength(20);

            builder.Property(x => x.Direccion)
                   .HasMaxLength(250);

            builder.Property(x => x.Departamento)
                   .HasMaxLength(100);

            builder.Property(x => x.Municipio)
                   .HasMaxLength(100);

            // Índices
            builder.HasIndex(x => x.Correo)
                   .IsUnique();

            builder.HasIndex(x => x.Apellidos);

            builder.HasIndex(x => x.Nombres);
        }
    }
}
