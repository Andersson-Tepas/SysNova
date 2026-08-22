using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using Google.Apis.Auth;
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

            _passwordHasher =
                new PasswordHasher<Cliente>();
        }


        // ==========================================
        // LOGIN NORMAL
        // ==========================================

        public async Task<string?> LoginAsync(
            LoginDTO login)
        {
            var correo =
                NormalizarCorreo(login.Correo);


            // ==========================================
            // 1. BUSCAR ADMINISTRADOR / USUARIO
            // ==========================================

            var usuario =
                await _context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u =>
                        u.Correo.ToLower() == correo &&
                        u.Activo);

            if (usuario != null)
            {
                // ======================================
                // LOGIN ADMIN ACTUAL
                // ======================================
                //
                // Lo dejamos como ya lo tenías.
                // Más adelante podemos hashear también
                // las contraseñas administrativas.
                //

                if (usuario.Password != login.Password)
                    return null;

                return GenerarToken(
                    usuario.UsuarioId.ToString(),
                    usuario.Correo,
                    usuario.Rol?.Nombre
                        ?? "Administrador");
            }


            // ==========================================
            // 2. BUSCAR CLIENTE
            // ==========================================

            var cliente =
                await _context.Clientes
                    .FirstOrDefaultAsync(c =>
                        c.Correo.ToLower() == correo &&
                        c.Activo);

            if (cliente == null)
                return null;


            // ==========================================
            // 3. VERIFICAR PASSWORD HASHEADO
            // ==========================================

            var resultado =
                _passwordHasher
                    .VerifyHashedPassword(
                        cliente,
                        cliente.Password,
                        login.Password);

            if (resultado ==
                PasswordVerificationResult.Failed)
            {
                return null;
            }


            // ==========================================
            // 4. GENERAR JWT CLIENTE
            // ==========================================

            return GenerarToken(
                cliente.ClienteId.ToString(),
                cliente.Correo,
                "Cliente");
        }


        // ==========================================
        // REGISTER NORMAL
        // ==========================================

        public async Task<bool> RegisterAsync(
            RegisterDTO register)
        {
            // ==========================================
            // VALIDAR PASSWORD EN BACKEND
            // ==========================================

            if (!EsPasswordSegura(
                    register.Password))
            {
                return false;
            }


            // ==========================================
            // NORMALIZAR CORREO
            // ==========================================

            var correo =
                NormalizarCorreo(
                    register.Correo);

            if (string.IsNullOrWhiteSpace(
                    correo))
            {
                return false;
            }


            // ==========================================
            // VERIFICAR CORREO EN USUARIOS
            // ==========================================

            var correoUsuario =
                await _context.Usuarios
                    .AnyAsync(u =>
                        u.Correo.ToLower() ==
                        correo);

            if (correoUsuario)
                return false;


            // ==========================================
            // VERIFICAR CORREO EN CLIENTES
            // ==========================================

            var correoCliente =
                await _context.Clientes
                    .AnyAsync(c =>
                        c.Correo.ToLower() ==
                        correo);

            if (correoCliente)
                return false;


            // ==========================================
            // CREAR CLIENTE
            // ==========================================

            var cliente =
                new Cliente
                {
                    Nombres =
                        register.Nombres.Trim(),

                    Apellidos =
                        string.IsNullOrWhiteSpace(
                            register.Apellidos)
                            ? "-"
                            : register
                                .Apellidos
                                .Trim(),

                    Correo =
                        correo,

                    Telefono =
                        register.Telefono,

                    Direccion =
                        register.Direccion,

                    Departamento =
                        register.Departamento,

                    Municipio =
                        register.Municipio,

                    Activo =
                        true,

                    FechaCreacion =
                        DateTime.UtcNow
                };


            // ==========================================
            // HASH PASSWORD
            // ==========================================

            cliente.Password =
                _passwordHasher
                    .HashPassword(
                        cliente,
                        register.Password);


            // ==========================================
            // GUARDAR
            // ==========================================

            _context.Clientes.Add(
                cliente);

            await _context
                .SaveChangesAsync();

            return true;
        }


        // ==========================================
        // GOOGLE LOGIN / REGISTRO
        // ==========================================

        public async Task<GoogleLoginResultDTO?>
            LoginGoogleAsync(
                GoogleCodeDTO googleLogin)
        {
            // ==========================================
            // 1. CONFIGURACIÓN GOOGLE
            // ==========================================

            var clientId =
                _configuration[
                    "GoogleAuth:ClientId"];

            var clientSecret =
                _configuration[
                    "GoogleAuth:ClientSecret"];

            var redirectUri =
                _configuration[
                    "GoogleAuth:RedirectUri"];


            if (string.IsNullOrWhiteSpace(
                    clientId))
            {
                throw new InvalidOperationException(
                    "GoogleAuth:ClientId no está configurado.");
            }


            if (string.IsNullOrWhiteSpace(
                    clientSecret))
            {
                throw new InvalidOperationException(
                    "GoogleAuth:ClientSecret no está configurado.");
            }


            if (string.IsNullOrWhiteSpace(
                    redirectUri))
            {
                throw new InvalidOperationException(
                    "GoogleAuth:RedirectUri no está configurado.");
            }


            // ==========================================
            // 2. INTERCAMBIAR CODE POR TOKENS GOOGLE
            // ==========================================

            var idToken =
                await ObtenerIdTokenGoogleAsync(
                    googleLogin.Code,
                    clientId,
                    clientSecret,
                    redirectUri);


            if (string.IsNullOrWhiteSpace(
                    idToken))
            {
                return null;
            }


            // ==========================================
            // 3. VALIDAR ID TOKEN GOOGLE
            // ==========================================

            GoogleJsonWebSignature.Payload
                payload;

            try
            {
                var settings =
                    new GoogleJsonWebSignature
                        .ValidationSettings
                    {
                        Audience =
                            new[]
                            {
                                clientId
                            }
                    };


                payload =
                    await GoogleJsonWebSignature
                        .ValidateAsync(
                            idToken,
                            settings);
            }
            catch (InvalidJwtException)
            {
                return null;
            }


            // ==========================================
            // 4. VALIDAR INFORMACIÓN GOOGLE
            // ==========================================

            if (!payload.EmailVerified)
                return null;

            if (string.IsNullOrWhiteSpace(
                    payload.Email))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(
                    payload.Subject))
            {
                return null;
            }


            var correo =
                NormalizarCorreo(
                    payload.Email);

            var googleSubject =
                payload.Subject.Trim();


            // ==========================================
            // 5. GOOGLE NO CREA ADMINISTRADORES
            // ==========================================

            var existeComoUsuario =
                await _context.Usuarios
                    .AnyAsync(u =>
                        u.Correo.ToLower() ==
                        correo);

            if (existeComoUsuario)
            {
                return null;
            }


            // ==========================================
            // 6. BUSCAR CLIENTE POR GOOGLE SUBJECT
            // ==========================================

            var cliente =
                await _context.Clientes
                    .FirstOrDefaultAsync(c =>
                        c.GoogleSubject ==
                        googleSubject);


            // ==========================================
            // 7. BUSCAR CUENTA EXISTENTE POR CORREO
            // ==========================================

            if (cliente == null)
            {
                cliente =
                    await _context.Clientes
                        .FirstOrDefaultAsync(c =>
                            c.Correo.ToLower() ==
                            correo);


                if (cliente != null)
                {
                    if (!cliente.Activo)
                        return null;


                    // Si ya tiene otra cuenta Google
                    // vinculada, rechazamos.
                    if (!string.IsNullOrWhiteSpace(
                            cliente.GoogleSubject) &&
                        cliente.GoogleSubject !=
                            googleSubject)
                    {
                        return null;
                    }


                    // Vinculamos Google a ese cliente.
                    cliente.GoogleSubject =
                        googleSubject;
                }
            }


            // ==========================================
            // 8. SI NO EXISTE: CREAR CLIENTE
            // ==========================================

            if (cliente == null)
            {
                cliente =
                    new Cliente
                    {
                        Nombres =
                            string.IsNullOrWhiteSpace(
                                payload.GivenName)
                                ? "Cliente"
                                : payload
                                    .GivenName
                                    .Trim(),

                        Apellidos =
                            string.IsNullOrWhiteSpace(
                                payload.FamilyName)
                                ? "-"
                                : payload
                                    .FamilyName
                                    .Trim(),

                        Correo =
                            correo,

                        GoogleSubject =
                            googleSubject,

                        Activo =
                            true,

                        FechaCreacion =
                            DateTime.UtcNow
                    };


                // Cliente.Password no admite null.
                // Para una cuenta Google generamos una
                // contraseña aleatoria que el usuario
                // nunca conoce y guardamos solo su hash.
                var passwordAleatorio =
                    Convert.ToBase64String(
                        RandomNumberGenerator
                            .GetBytes(32));


                cliente.Password =
                    _passwordHasher
                        .HashPassword(
                            cliente,
                            passwordAleatorio);


                _context.Clientes.Add(
                    cliente);
            }


            // ==========================================
            // 9. VALIDAR CLIENTE ACTIVO
            // ==========================================

            if (!cliente.Activo)
                return null;


            // ==========================================
            // 10. ACTUALIZAR DATOS DESDE GOOGLE
            // ==========================================

            if (!string.IsNullOrWhiteSpace(
                    payload.GivenName))
            {
                cliente.Nombres =
                    payload
                        .GivenName
                        .Trim();
            }


            if (!string.IsNullOrWhiteSpace(
                    payload.FamilyName))
            {
                cliente.Apellidos =
                    payload
                        .FamilyName
                        .Trim();
            }


            cliente.GoogleSubject =
                googleSubject;


            // ==========================================
            // 11. GUARDAR EN SQL SERVER
            // ==========================================

            await _context
                .SaveChangesAsync();


            // ==========================================
            // 12. GENERAR JWT NEXOTECH
            // ==========================================

            var token =
                GenerarToken(
                    cliente
                        .ClienteId
                        .ToString(),

                    cliente.Correo,

                    "Cliente");


            var nombreCompleto =
                $"{cliente.Nombres} {cliente.Apellidos}"
                    .Trim();


            return new GoogleLoginResultDTO
            {
                Token =
                    token,

                Correo =
                    cliente.Correo,

                Nombre =
                    nombreCompleto
            };
        }


        // ==========================================
        // GOOGLE - INTERCAMBIAR AUTHORIZATION CODE
        // POR ID TOKEN
        // ==========================================

        private static async Task<string?>
            ObtenerIdTokenGoogleAsync(
                string code,
                string clientId,
                string clientSecret,
                string redirectUri)
        {
            if (string.IsNullOrWhiteSpace(
                    code))
            {
                return null;
            }


            using var httpClient =
                new HttpClient();


            using var contenido =
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        ["code"] =
                            code,

                        ["client_id"] =
                            clientId,

                        ["client_secret"] =
                            clientSecret,

                        ["redirect_uri"] =
                            redirectUri,

                        ["grant_type"] =
                            "authorization_code"
                    });


            var response =
                await httpClient.PostAsync(
                    "https://oauth2.googleapis.com/token",
                    contenido);


            var json =
                await response.Content
                    .ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"Google Token Error: {json}");

                return null;
            }


            using var documento =
                JsonDocument.Parse(
                    json);


            if (!documento
                .RootElement
                .TryGetProperty(
                    "id_token",
                    out var idTokenElement))
            {
                Console.WriteLine(
                    "Google no devolvió id_token.");

                return null;
            }


            return idTokenElement
                .GetString();
        }


        // ==========================================
        // GENERAR JWT
        // ==========================================

        private string GenerarToken(
            string id,
            string correo,
            string rol)
        {
            var claims =
                new List<Claim>
                {
                    new Claim(
                        ClaimTypes
                            .NameIdentifier,
                        id),

                    new Claim(
                        ClaimTypes.Name,
                        correo),

                    new Claim(
                        ClaimTypes.Role,
                        rol)
                };


            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration[
                            "Jwt:Key"]!));


            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms
                        .HmacSha256);


            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration[
                            "Jwt:Issuer"],

                    audience:
                        _configuration[
                            "Jwt:Audience"],

                    claims:
                        claims,

                    expires:
                        DateTime.UtcNow
                            .AddHours(8),

                    signingCredentials:
                        credentials);


            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }


        // ==========================================
        // NORMALIZAR CORREO
        // ==========================================

        private static string NormalizarCorreo(
            string? correo)
        {
            return correo?
                .Trim()
                .ToLowerInvariant()
                ?? string.Empty;
        }


        // ==========================================
        // PASSWORD FUERTE
        // ==========================================

        private static bool EsPasswordSegura(
            string? password)
        {
            if (string.IsNullOrWhiteSpace(
                    password))
            {
                return false;
            }


            return Regex.IsMatch(
                password,

                @"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$");
        }
    }
}