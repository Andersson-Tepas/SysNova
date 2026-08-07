using Microsoft.EntityFrameworkCore;
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