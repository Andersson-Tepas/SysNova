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

        public BannerController(IBannerService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BannerDTO>>> GetAll()
        {
            var banners = await _service.GetAllAsync();
            return Ok(banners);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<BannerDTO>> GetById(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null)
                return NotFound();

            return Ok(banner);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<BannerDTO>> Create(BannerDTO bannerDto)
        {
            var nuevoBanner = await _service.AddAsync(bannerDto);
            return Ok(nuevoBanner);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(BannerDTO bannerDto)
        {
            var existente = await _service.GetByIdAsync(bannerDto.BannerId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(bannerDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}