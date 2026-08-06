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
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
    {
        public void Configure(EntityTypeBuilder<DetallePedido> builder)
        {
            // Nombre de la tabla
            builder.ToTable("DetallePedido");

            // Llave primaria
            builder.HasKey(x => x.DetallePedidoId);

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
            builder.HasOne(x => x.Pedido)
                   .WithMany(x => x.Detalles)
                   .HasForeignKey(x => x.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Producto)
                   .WithMany(x => x.DetallesPedido)
                   .HasForeignKey(x => x.ProductoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(x => x.PedidoId);

            builder.HasIndex(x => x.ProductoId);

            builder.HasIndex(x => new { x.PedidoId, x.ProductoId });
        }
    }
} 