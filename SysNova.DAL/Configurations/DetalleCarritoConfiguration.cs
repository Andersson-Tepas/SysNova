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
    public class DetalleCarritoConfiguration : IEntityTypeConfiguration<DetalleCarrito>
    {
        public void Configure(EntityTypeBuilder<DetalleCarrito> builder)
        {
            // Nombre de la tabla
            builder.ToTable("DetalleCarrito");

            // Llave primaria
            builder.HasKey(x => x.DetalleCarritoId);

            // Propiedades
            builder.Property(x => x.Cantidad)
                   .IsRequired();

            builder.Property(x => x.PrecioUnitario)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.SubTotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Relaciones
            builder.HasOne(x => x.Carrito)
                   .WithMany(x => x.Detalles)
                   .HasForeignKey(x => x.CarritoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Producto)
                   .WithMany(x => x.DetallesCarrito)
                   .HasForeignKey(x => x.ProductoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(x => x.CarritoId);

            builder.HasIndex(x => x.ProductoId);

            builder.HasIndex(x => new { x.CarritoId, x.ProductoId })
                   .IsUnique();
        }
    }
} 