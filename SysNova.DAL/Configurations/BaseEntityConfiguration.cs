using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysNova.EN.Entities;
using System;

namespace SysNova.DAL.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Configuración de propiedades auditables comunes

            builder.Property(x => x.Activo)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(x => x.FechaCreacion)
                   .IsRequired()
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()"); // Para SQL Server (o cambia por CURRENT_TIMESTAMP si usas PostgreSQL/MySQL)

            builder.Property(x => x.FechaModificacion)
                   .IsRequired(false)
                   .HasColumnType("datetime");

            // Índice por defecto para filtrado por estado activo
            builder.HasIndex(x => x.Activo);
        }
    }
}
