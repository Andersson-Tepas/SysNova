using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ==========================================
        // LOGIN
        // ==========================================

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest(new
                {
                    mensaje = "Los datos de inicio de sesión son requeridos."
                });
            }

            var token = await _authService.LoginAsync(login);

            if (token == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }

            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso.",
                token = token
            });
        }

        // ==========================================
        // REGISTER
        // ==========================================

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if (register == null)
            {
                return BadRequest(new
                {
                    mensaje = "Los datos de registro son requeridos."
                });
            }

            var resultado = await _authService.RegisterAsync(register);

            if (!resultado)
            {
                return BadRequest(new
                {
                    mensaje = "No se pudo registrar el usuario. El correo podría ya estar registrado."
                });
            }

            return Ok(new
            {
                mensaje = "Registro exitoso."
            });
        }
    }
}