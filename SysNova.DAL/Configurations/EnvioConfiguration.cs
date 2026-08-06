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
    public class EnvioConfiguration : IEntityTypeConfiguration<Envio>
    {
        public void Configure(EntityTypeBuilder<Envio> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Envio");

            // Llave primaria
            builder.HasKey(x => x.EnvioId);

            // Propiedades
            builder.Property(x => x.EmpresaTransportista)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.NumeroGuia)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Estado)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(x => x.FechaSalida);

            builder.Property(x => x.FechaEntrega);

            // Relaciones
            builder.HasOne(x => x.Pedido)
                   .WithOne(x => x.Envio)
                   .HasForeignKey<Envio>(x => x.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(x => x.PedidoId)
                   .IsUnique();

            builder.HasIndex(x => x.NumeroGuia)
                   .IsUnique();

            builder.HasIndex(x => x.Estado);
        }
    }
} 