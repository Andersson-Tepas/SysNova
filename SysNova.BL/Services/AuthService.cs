using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SysNova.BL.Interfaces;
using SysNova.DAL.Context;
using SysNova.DTO;

namespace SysNova.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly SysNovaDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            SysNovaDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginDTO login)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u =>
                    u.Correo == login.Correo &&
                    u.Activo);

            if (usuario == null)
                return null;

            // TEMPORAL:
            // Actualmente tu BD tiene contraseñas sin hash.
            if (usuario.Password != login.Password)
                return null;

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.UsuarioId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    usuario.Correo),

                new Claim(
                    ClaimTypes.Role,
                    usuario.Rol?.Nombre ?? string.Empty)
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