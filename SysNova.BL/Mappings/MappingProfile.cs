using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SysNova.DTO;
using SysNova.EN.Entities; // Asegúrate de ajustar este namespace si tus entidades están en otra subcarpeta

namespace SysNova.BL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Catálogo
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<ImagenProducto, ImagenProductoDTO>().ReverseMap();

            // Clientes
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            // Seguridad
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();

            // Ventas
            CreateMap<Carrito, CarritoDTO>().ReverseMap();
            CreateMap<DetalleCarrito, DetalleCarritoDTO>().ReverseMap();
            CreateMap<MetodoPago, MetodoPagoDTO>().ReverseMap();
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<DetallePedido, DetallePedidoDTO>().ReverseMap();
            CreateMap<Favorito, FavoritoDTO>().ReverseMap();
            CreateMap<Resena, ResenaDTO>().ReverseMap();
            CreateMap<Envio, EnvioDTO>().ReverseMap();

            // Sitio Web
            CreateMap<Banner, BannerDTO>().ReverseMap();
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<PreguntaFrecuente, PreguntaFrecuenteDTO>().ReverseMap();
            CreateMap<Contacto, ContactoDTO>().ReverseMap();
        }
    }
}
