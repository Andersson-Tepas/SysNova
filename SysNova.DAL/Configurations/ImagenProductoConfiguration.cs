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
    public class ImagenProductoConfiguration : IEntityTypeConfiguration<ImagenProducto>
    {
        public void Configure(EntityTypeBuilder<ImagenProducto> builder)
        {
            // Nombre de la tabla
            builder.ToTable("ImagenProducto");

            // Llave primaria
            builder.HasKey(x => x.ImagenProductoId);

            // Propiedades
            builder.Property(x => x.UrlImagen)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.Orden)
                   .IsRequired();

            // Relaciones
            builder.HasOne(x => x.Producto)
                   .WithMany(x => x.Imagenes)
                   .HasForeignKey(x => x.ProductoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(x => x.ProductoId);

            builder.HasIndex(x => new { x.ProductoId, x.Orden })
                   .IsUnique();
        }
    }
}
