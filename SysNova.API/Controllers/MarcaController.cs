using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _service;
        private readonly IWebHostEnvironment _environment;

        private const long MaxLogoSize =
            5 * 1024 * 1024;

        private static readonly string[] ExtensionesPermitidas =
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".webp"
        };

        public MarcaController(
            IMarcaService service,
            IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> GetAll()
        {
            var marcas =
                await _service.GetAllAsync();

            return Ok(marcas);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MarcaDTO>> GetById(int id)
        {
            var marca =
                await _service.GetByIdAsync(id);

            if (marca == null)
                return NotFound();

            return Ok(marca);
        }

        // ==========================================
        // SUBIR LOGO
        // ==========================================

        [HttpPost("upload-logo")]
        [Authorize(Roles = "Administrador")]
        [RequestSizeLimit(MaxLogoSize)]
        public async Task<IActionResult> UploadLogo(
            IFormFile archivo)
        {
            if (archivo == null ||
                archivo.Length == 0)
            {
                return BadRequest(
                    "Debes seleccionar una imagen.");
            }

            if (archivo.Length > MaxLogoSize)
            {
                return BadRequest(
                    "La imagen no puede superar los 5 MB.");
            }

            var extension =
                Path.GetExtension(
                    archivo.FileName)
                .ToLowerInvariant();

            if (!ExtensionesPermitidas.Contains(
                    extension))
            {
                return BadRequest(
                    "Formato no permitido. Usa PNG, JPG, JPEG o WEBP.");
            }

            var contentTypesPermitidos =
                new[]
                {
                    "image/png",
                    "image/jpeg",
                    "image/webp"
                };

            if (string.IsNullOrWhiteSpace(
                    archivo.ContentType) ||
                !contentTypesPermitidos.Contains(
                    archivo.ContentType,
                    StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest(
                    "El archivo seleccionado no es una imagen válida.");
            }

            var webRoot =
                _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(webRoot))
            {
                webRoot =
                    Path.Combine(
                        _environment.ContentRootPath,
                        "wwwroot");
            }

            var carpeta =
                Path.Combine(
                    webRoot,
                    "images",
                    "marcas");

            Directory.CreateDirectory(
                carpeta);

            var nombreArchivo =
                $"{Guid.NewGuid():N}{extension}";

            var rutaFisica =
                Path.Combine(
                    carpeta,
                    nombreArchivo);

            await using (
                var stream =
                    new FileStream(
                        rutaFisica,
                        FileMode.Create))
            {
                await archivo.CopyToAsync(
                    stream);
            }

            var url =
                $"{Request.Scheme}://{Request.Host}/images/marcas/{nombreArchivo}";

            return Ok(
                new
                {
                    Url = url,
                    NombreArchivo = nombreArchivo
                });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<MarcaDTO>> Create(
            MarcaDTO marcaDto)
        {
            var nuevaMarca =
                await _service.AddAsync(
                    marcaDto);

            return Ok(
                nuevaMarca);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            MarcaDTO marcaDto)
        {
            var existente =
                await _service.GetByIdAsync(
                    marcaDto.MarcaId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(
                marcaDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var marca =
                await _service.GetByIdAsync(
                    id);

            if (marca == null)
                return NotFound();

            await _service.DeleteAsync(
                id);

            return NoContent();
        }
    }
}