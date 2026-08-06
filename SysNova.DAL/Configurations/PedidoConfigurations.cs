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
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Pedido");

            // Llave primaria
            builder.HasKey(x => x.PedidoId);

            // Propiedades
            builder.Property(x => x.NumeroPedido)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(x => x.FechaPedido)
                   .IsRequired();

            builder.Property(x => x.SubTotal)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IVA)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Descuento)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Total)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Estado)
                   .HasConversion<int>()
                   .IsRequired();

            // Relaciones
            builder.HasOne(x => x.Cliente)
                   .WithMany(x => x.Pedidos)
                   .HasForeignKey(x => x.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.MetodoPago)
                   .WithMany(x => x.Pedidos)
                   .HasForeignKey(x => x.MetodoPagoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Envio)
                   .WithOne(x => x.Pedido)
                   .HasForeignKey<Envio>(x => x.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(x => x.NumeroPedido)
                   .IsUnique();

            builder.HasIndex(x => x.FechaPedido);

            builder.HasIndex(x => x.Estado);
        }
    }
}