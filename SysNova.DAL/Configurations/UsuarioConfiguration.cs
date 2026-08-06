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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Usuario");

            // Llave primaria
            builder.HasKey(x => x.UsuarioId);

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

            // Relaciones
            builder.HasOne(x => x.Rol)
                   .WithMany(x => x.Usuarios)
                   .HasForeignKey(x => x.RolId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(x => x.Correo)
                   .IsUnique();

            builder.HasIndex(x => x.RolId);
        }
    }
}