using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
        private readonly IWebHostEnvironment _environment;

        private const long MaxImagenSize =
            5 * 1024 * 1024;

        private static readonly string[] ExtensionesPermitidas =
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".webp"
        };

        public CategoriaController(
            ICategoriaService service,
            IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
        {
            var categorias =
                await _service.GetAllAsync();

            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            var categoria =
                await _service.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost("upload-imagen")]
        [Authorize(Roles = "Administrador")]
        [RequestSizeLimit(MaxImagenSize)]
        public async Task<IActionResult> UploadImagen(
            IFormFile archivo)
        {
            if (archivo == null ||
                archivo.Length == 0)
            {
                return BadRequest(
                    "Debes seleccionar una imagen.");
            }

            if (archivo.Length > MaxImagenSize)
            {
                return BadRequest(
                    "La imagen no puede superar los 5 MB.");
            }

            var extension =
                Path.GetExtension(archivo.FileName)
                    .ToLowerInvariant();

            if (!ExtensionesPermitidas.Contains(extension))
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

            if (string.IsNullOrWhiteSpace(archivo.ContentType) ||
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
                    "categorias");

            Directory.CreateDirectory(carpeta);

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
                await archivo.CopyToAsync(stream);
            }

            var url =
                $"{Request.Scheme}://{Request.Host}/images/categorias/{nombreArchivo}";

            return Ok(
                new
                {
                    Url = url,
                    NombreArchivo = nombreArchivo
                });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoriaDTO>> Create(
            CategoriaDTO categoriaDto)
        {
            var nuevaCategoria =
                await _service.AddAsync(categoriaDto);

            return Ok(nuevaCategoria);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            CategoriaDTO categoriaDto)
        {
            var existente =
                await _service.GetByIdAsync(
                    categoriaDto.CategoriaId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(categoriaDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria =
                await _service.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}