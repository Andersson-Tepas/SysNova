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
    public class FavoritoConfiguration : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Favorito");

            // Llave primaria
            builder.HasKey(x => x.FavoritoId);

            // Propiedades
            builder.Property(x => x.FechaAgregado)
                   .IsRequired();

            // Relaciones
            builder.HasOne(x => x.Cliente)
                   .WithMany(x => x.Favoritos)
                   .HasForeignKey(x => x.ClienteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Producto)
                   .WithMany(x => x.Favoritos)
                   .HasForeignKey(x => x.ProductoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(x => x.ClienteId);

            builder.HasIndex(x => x.ProductoId);

            builder.HasIndex(x => new { x.ClienteId, x.ProductoId })
                   .IsUnique();
        }
    }
}
