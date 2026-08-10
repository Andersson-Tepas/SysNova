using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SysNova.BL.Interfaces;
using SysNova.DAL.Context;
using SysNova.DTO;
using SysNova.EN.Entities;

namespace SysNova.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly SysNovaDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Cliente> _passwordHasher;

        public AuthService(
            SysNovaDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<Cliente>();
        }

        // ==========================================
        // LOGIN
        // ==========================================

        public async Task<string?> LoginAsync(LoginDTO login)
        {
            // ==========================================
            // 1. BUSCAR ADMINISTRADOR / USUARIO
            // ==========================================

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u =>
                    u.Correo == login.Correo &&
                    u.Activo);

            if (usuario != null)
            {
                // Los usuarios administrativos actuales
                // todavía tienen contraseña sin hash.

                if (usuario.Password != login.Password)
                    return null;

                return GenerarToken(
                    usuario.UsuarioId.ToString(),
                    usuario.Correo,
                    usuario.Rol?.Nombre ?? "Administrador");
            }

            // ==========================================
            // 2. BUSCAR CLIENTE
            // ==========================================

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c =>
                    c.Correo == login.Correo &&
                    c.Activo);

            if (cliente == null)
                return null;

            // ==========================================
            // 3. VERIFICAR PASSWORD HASHEADO
            // ==========================================

            var resultado = _passwordHasher.VerifyHashedPassword(
                cliente,
                cliente.Password,
                login.Password);

            if (resultado == PasswordVerificationResult.Failed)
                return null;

            // ==========================================
            // 4. GENERAR TOKEN DE CLIENTE
            // ==========================================

            return GenerarToken(
                cliente.ClienteId.ToString(),
                cliente.Correo,
                "Cliente");
        }

        // ==========================================
        // REGISTER
        // ==========================================

        public async Task<bool> RegisterAsync(RegisterDTO register)
        {
            // ==========================================
            // VERIFICAR CORREO EN USUARIOS
            // ==========================================

            var correoUsuario = await _context.Usuarios
                .AnyAsync(u => u.Correo == register.Correo);

            if (correoUsuario)
                return false;

            // ==========================================
            // VERIFICAR CORREO EN CLIENTES
            // ==========================================

            var correoCliente = await _context.Clientes
                .AnyAsync(c => c.Correo == register.Correo);

            if (correoCliente)
                return false;

            // ==========================================
            // CREAR CLIENTE
            // ==========================================

            var cliente = new Cliente
            {
                Nombres = register.Nombres,
                Apellidos = register.Apellidos,
                Correo = register.Correo,
                Telefono = register.Telefono,
                Direccion = register.Direccion,
                Departamento = register.Departamento,
                Municipio = register.Municipio,

                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            // ==========================================
            // HASH DE PASSWORD
            // ==========================================

            cliente.Password = _passwordHasher.HashPassword(
                cliente,
                register.Password);

            // ==========================================
            // GUARDAR
            // ==========================================

            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // GENERAR JWT
        // ==========================================

        private string GenerarToken(
            string id,
            string correo,
            string rol)
        {
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    id),

                new Claim(
                    ClaimTypes.Name,
                    correo),

                new Claim(
                    ClaimTypes.Role,
                    rol)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}