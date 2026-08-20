using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly IWebHostEnvironment _environment;

        private const long MaxImagenSize =
            8 * 1024 * 1024;

        private static readonly string[] ExtensionesPermitidas =
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".webp"
        };

        public BlogController(
            IBlogService service,
            IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetAll()
        {
            var blogs =
                await _service.GetAllAsync();

            return Ok(blogs);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<BlogDTO>> GetById(int id)
        {
            var blog =
                await _service.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            return Ok(blog);
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
                    "La imagen no puede superar los 8 MB.");
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
                    "blogs");

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
                $"{Request.Scheme}://{Request.Host}/images/blogs/{nombreArchivo}";

            return Ok(
                new
                {
                    Url = url,
                    NombreArchivo = nombreArchivo
                });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<BlogDTO>> Create(
            BlogDTO blogDto)
        {
            var nuevoBlog =
                await _service.AddAsync(blogDto);

            return Ok(nuevoBlog);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            BlogDTO blogDto)
        {
            var existente =
                await _service.GetByIdAsync(
                    blogDto.BlogId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(blogDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var blog =
                await _service.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}