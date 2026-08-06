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
    public class ResenaConfiguration : IEntityTypeConfiguration<Resena>
    {
        public void Configure(EntityTypeBuilder<Resena> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Resena");

            // Llave primaria
            builder.HasKey(x => x.ResenaId);

            // Propiedades
            builder.Property(x => x.Calificacion)
                   .IsRequired();

            builder.Property(x => x.Comentario)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.Fecha)
                   .IsRequired();

            // Relaciones
            builder.HasOne(x => x.Cliente)
                   .WithMany(x => x.Resenas)
                   .HasForeignKey(x => x.ClienteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Producto)
                   .WithMany(x => x.Resenas)
                   .HasForeignKey(x => x.ProductoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(x => x.ClienteId);

            builder.HasIndex(x => x.ProductoId);

            builder.HasIndex(x => x.Fecha);

            builder.HasIndex(x => new { x.ClienteId, x.ProductoId })
                   .IsUnique();
        }
    }
}
