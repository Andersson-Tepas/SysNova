using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<Banner>>> GetAll()
        {
            var banners = await _service.GetAllAsync();
            return Ok(banners);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Banner>> GetById(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null)
                return NotFound();

            return Ok(banner);
        }

        [HttpPost]
        public async Task<ActionResult<Banner>> Create(Banner banner)
        {
            var nuevoBanner = await _service.AddAsync(banner);
            return Ok(nuevoBanner);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Banner banner)
        {
            await _service.UpdateAsync(banner);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null)
                return NotFound();

            await _service.DeleteAsync(banner);

            return NoContent();
        }
    }
}