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
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Producto");

            // Llave primaria
            builder.HasKey(x => x.ProductoId);

            // Propiedades
            builder.Property(x => x.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Descripcion)
                   .HasMaxLength(1000);

            builder.Property(x => x.CodigoSKU)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.CodigoBarra)
                   .HasMaxLength(50);

            builder.Property(x => x.Precio)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PrecioAnterior)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PorcentajeDescuento)
                   .HasColumnType("decimal(5,2)");

            builder.Property(x => x.ImagenPrincipal)
                   .HasMaxLength(300);

            // Relaciones
            builder.HasOne(x => x.Categoria)
                   .WithMany(x => x.Productos)
                   .HasForeignKey(x => x.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Marca)
                   .WithMany(x => x.Productos)
                   .HasForeignKey(x => x.MarcaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(x => x.Nombre);

            builder.HasIndex(x => x.CodigoSKU)
                   .IsUnique();
        }
    }
}
