using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysNova.EN.Entities;

using Microsoft.EntityFrameworkCore;

namespace SysNova.DAL.Context
{
    public class SysNovaDbContext : DbContext
    {
        public SysNovaDbContext(
            DbContextOptions<SysNovaDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            // ==========================================
            // CONFIGURACIONES DE ENTIDADES
            // ==========================================

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SysNovaDbContext).Assembly);


            // ==========================================
            // GOOGLE AUTH - CLIENTE
            // ==========================================
            //
            // GoogleSubject corresponde al claim "sub"
            // entregado por Google.
            //
            // Debe ser único cuando tenga un valor,
            // pero permitimos NULL porque los clientes
            // registrados normalmente todavía no tendrán
            // una cuenta Google vinculada.
            //

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.GoogleSubject)
                .IsUnique()
                .HasFilter("[GoogleSubject] IS NOT NULL");


            // ==========================================
            // BASE
            // ==========================================

            base.OnModelCreating(modelBuilder);
        }


        #region Catalogo

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<ImagenProducto> ImagenesProducto { get; set; }

        #endregion


        #region Clientes

        public DbSet<Cliente> Clientes { get; set; }

        #endregion


        #region Seguridad

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        #endregion


        #region Ventas

        public DbSet<Carrito> Carritos { get; set; }

        public DbSet<DetalleCarrito> DetallesCarrito { get; set; }

        public DbSet<MetodoPago> MetodosPago { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<DetallePedido> DetallesPedido { get; set; }

        public DbSet<Favorito> Favoritos { get; set; }

        public DbSet<Resena> Resenas { get; set; }

        public DbSet<Envio> Envios { get; set; }

        #endregion


        #region Sitio Web

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<PreguntaFrecuente> PreguntasFrecuentes { get; set; }

        public DbSet<Contacto> Contactos { get; set; }

        #endregion
    }
}