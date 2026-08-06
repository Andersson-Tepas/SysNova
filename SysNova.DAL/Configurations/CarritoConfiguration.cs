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
    public class CarritoConfiguration : IEntityTypeConfiguration<Carrito>
    {
        public void Configure(EntityTypeBuilder<Carrito> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Carrito");

            // Llave primaria
            builder.HasKey(x => x.CarritoId);

            // Propiedades
            builder.Property(x => x.Fecha)
                   .IsRequired();

            // Relaciones
            builder.HasOne(x => x.Cliente)
                   .WithMany(x => x.Carritos)
                   .HasForeignKey(x => x.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(x => x.ClienteId);

            builder.HasIndex(x => x.Fecha);
        }
    }
}
