using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SysNova.EN.Entities;

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

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.GoogleSubject)
                .IsUnique()
                .HasFilter("[GoogleSubject] IS NOT NULL");


            // ==========================================
            // BASE
            // ==========================================

            base.OnModelCreating(
                modelBuilder);
        }


        // ==========================================
        // CATÁLOGO
        // ==========================================

        #region Catalogo

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<ImagenProducto> ImagenesProducto { get; set; }

        #endregion


        // ==========================================
        // CLIENTES
        // ==========================================

        #region Clientes

        public DbSet<Cliente> Clientes { get; set; }

        #endregion


        // ==========================================
        // SEGURIDAD
        // ==========================================

        #region Seguridad

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        #endregion


        // ==========================================
        // VENTAS / CHECKOUT
        // ==========================================

        #region Ventas

        public DbSet<Carrito> Carritos { get; set; }

        public DbSet<DetalleCarrito> DetallesCarrito { get; set; }


        // Método seleccionado en checkout:
        // Efectivo / Tarjeta / PayPal
        public DbSet<MetodoPago> MetodosPago { get; set; }


        // Cabecera del pedido.
        public DbSet<Pedido> Pedidos { get; set; }


        // Productos pertenecientes al pedido.
        public DbSet<DetallePedido> DetallesPedido { get; set; }


        public DbSet<Favorito> Favoritos { get; set; }

        public DbSet<Resena> Resenas { get; set; }


        // Después nos servirá para la pantalla
        // de seguimiento del pedido.
        public DbSet<Envio> Envios { get; set; }

        #endregion


        // ==========================================
        // SITIO WEB
        // ==========================================

        #region SitioWeb

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<PreguntaFrecuente> PreguntasFrecuentes { get; set; }

        public DbSet<Contacto> Contactos { get; set; }

        #endregion
    }
}