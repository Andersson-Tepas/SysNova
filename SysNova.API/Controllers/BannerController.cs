using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _service;
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

        public BannerController(
            IBannerService service,
            IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BannerDTO>>> GetAll()
        {
            var banners =
                await _service.GetAllAsync();

            return Ok(banners);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<BannerDTO>> GetById(int id)
        {
            var banner =
                await _service.GetByIdAsync(id);

            if (banner == null)
                return NotFound();

            return Ok(banner);
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
                    "banners");

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
                $"{Request.Scheme}://{Request.Host}/images/banners/{nombreArchivo}";

            return Ok(
                new
                {
                    Url = url,
                    NombreArchivo = nombreArchivo
                });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<BannerDTO>> Create(
            BannerDTO bannerDto)
        {
            var nuevoBanner =
                await _service.AddAsync(bannerDto);

            return Ok(nuevoBanner);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            BannerDTO bannerDto)
        {
            var existente =
                await _service.GetByIdAsync(
                    bannerDto.BannerId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(bannerDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var banner =
                await _service.GetByIdAsync(id);

            if (banner == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}