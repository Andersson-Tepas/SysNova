using Microsoft.EntityFrameworkCore;
using SysNova.BL.Interfaces;
using SysNova.BL.Services;
using SysNova.DAL.Context;
using SysNova.Repository.Interfaces;
using SysNova.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// DATABASE
// ==========================================

builder.Services.AddDbContext<SysNovaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// REPOSITORIES
// ==========================================

// Catálogo
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IImagenProductoRepository, ImagenProductoRepository>();

// Clientes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Seguridad
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();

// Ventas
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<IDetalleCarritoRepository, DetalleCarritoRepository>();
builder.Services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddScoped<IResenaRepository, ResenaRepository>();
builder.Services.AddScoped<IEnvioRepository, EnvioRepository>();

// Sitio Web
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPreguntaFrecuenteRepository, PreguntaFrecuenteRepository>();
builder.Services.AddScoped<IContactoRepository, ContactoRepository>();

// ==========================================
// SERVICES - BUSINESS LOGIC
// ==========================================

// Catálogo
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IImagenProductoService, ImagenProductoService>();

// Clientes
builder.Services.AddScoped<IClienteService, ClienteService>();

// Seguridad
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();

// Ventas
builder.Services.AddScoped<ICarritoService, CarritoService>();
builder.Services.AddScoped<IDetalleCarritoService, DetalleCarritoService>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IDetallePedidoService, DetallePedidoService>();
builder.Services.AddScoped<IFavoritoService, FavoritoService>();
builder.Services.AddScoped<IResenaService, ResenaService>();
builder.Services.AddScoped<IEnvioService, EnvioService>();

// Sitio Web
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IPreguntaFrecuenteService, PreguntaFrecuenteService>();
builder.Services.AddScoped<IContactoService, ContactoService>();

// ==========================================
// CONTROLLERS
// ==========================================

builder.Services.AddControllers();

// ==========================================
// SWAGGER
// ==========================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================================
// HTTP REQUEST PIPELINE
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();