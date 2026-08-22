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

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }


        // ==========================================
        // LOGIN NORMAL
        // ==========================================

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest(new
                {
                    mensaje =
                        "Los datos de inicio de sesión son requeridos."
                });
            }

            var token =
                await _authService
                    .LoginAsync(login);

            if (token == null)
            {
                return Unauthorized(new
                {
                    mensaje =
                        "Correo o contraseña incorrectos."
                });
            }

            return Ok(new
            {
                mensaje =
                    "Inicio de sesión exitoso.",

                token =
                    token
            });
        }


        // ==========================================
        // LOGIN / REGISTRO CON GOOGLE
        // ==========================================

        [HttpPost("google")]
        public async Task<IActionResult> Google(
            [FromBody] GoogleCodeDTO googleLogin)
        {
            if (googleLogin == null ||
                string.IsNullOrWhiteSpace(
                    googleLogin.Code))
            {
                return BadRequest(new
                {
                    mensaje =
                        "El código de Google es requerido."
                });
            }

            try
            {
                var resultado =
                    await _authService
                        .LoginGoogleAsync(
                            googleLogin);

                if (resultado == null)
                {
                    return Unauthorized(new
                    {
                        mensaje =
                            "No se pudo validar la cuenta de Google."
                    });
                }

                return Ok(new
                {
                    mensaje =
                        "Inicio de sesión con Google exitoso.",

                    token =
                        resultado.Token,

                    correo =
                        resultado.Correo,

                    nombre =
                        resultado.Nombre
                });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error Google Auth: {ex.Message}");

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al procesar el inicio de sesión con Google."
                    });
            }
        }


        // ==========================================
        // REGISTER NORMAL
        // ==========================================

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDTO register)
        {
            if (register == null)
            {
                return BadRequest(new
                {
                    mensaje =
                        "Los datos de registro son requeridos."
                });
            }

            if (!ModelState.IsValid)
            {
                var errores =
                    ModelState
                        .Values
                        .SelectMany(x =>
                            x.Errors)
                        .Select(x =>
                            x.ErrorMessage)
                        .Where(x =>
                            !string.IsNullOrWhiteSpace(x))
                        .ToList();

                return BadRequest(new
                {
                    mensaje =
                        errores.FirstOrDefault()
                        ?? "Los datos del registro no son válidos.",

                    errores =
                        errores
                });
            }

            var resultado =
                await _authService
                    .RegisterAsync(
                        register);

            if (!resultado)
            {
                return BadRequest(new
                {
                    mensaje =
                        "No se pudo registrar el usuario. El correo podría ya estar registrado o la contraseña no cumple los requisitos."
                });
            }

            return Ok(new
            {
                mensaje =
                    "Registro exitoso."
            });
        }
    }
}