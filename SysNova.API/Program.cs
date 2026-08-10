using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SysNova.BL.Interfaces;
using SysNova.BL.Mappings;
using SysNova.BL.Services;
using SysNova.DAL.Context;
using SysNova.Repository.Interfaces;
using SysNova.Repository.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// DATABASE
// ==========================================

builder.Services.AddDbContext<SysNovaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// AUTOMAPPER
// ==========================================

builder.Services.AddAutoMapper(cfg =>
    cfg.AddProfile<MappingProfile>());

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
// AUTH SERVICE
// ==========================================

builder.Services.AddScoped<IAuthService, AuthService>();

// ==========================================
// JWT AUTHENTICATION
// ==========================================

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "La clave JWT no está configurada en appsettings.json.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException(
        "El Issuer JWT no está configurado en appsettings.json.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException(
        "El Audience JWT no está configurado en appsettings.json.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ClockSkew = TimeSpan.Zero
        };
    });

// ==========================================
// AUTHORIZATION
// ==========================================

builder.Services.AddAuthorization();

// ==========================================
// CONTROLLERS
// ==========================================

builder.Services.AddControllers();

// ==========================================
// SWAGGER
// ==========================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SysNova API",
        Version = "v1"
    });

    // Configuración JWT para Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escribe: Bearer {tu token JWT}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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

// ==========================================
// AUTHENTICATION & AUTHORIZATION
// ==========================================

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();