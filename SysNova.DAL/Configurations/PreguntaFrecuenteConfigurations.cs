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
    public class PreguntaFrecuenteConfiguration : IEntityTypeConfiguration<PreguntaFrecuente>
    {
        public void Configure(EntityTypeBuilder<PreguntaFrecuente> builder)
        {
            // Nombre de la tabla
            builder.ToTable("PreguntaFrecuente");

            // Llave primaria
            builder.HasKey(x => x.PreguntaFrecuenteId);

            // Propiedades
            builder.Property(x => x.Pregunta)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.Respuesta)
                   .HasMaxLength(2000)
                   .IsRequired();

            builder.Property(x => x.Orden)
                   .IsRequired();

            builder.Property(x => x.Mostrar)
                   .IsRequired();

            // Índices
            builder.HasIndex(x => x.Orden);

            builder.HasIndex(x => x.Mostrar);
        }
    }
}
