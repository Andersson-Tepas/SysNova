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

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetAll()
        {
            var blogs = await _service.GetAllAsync();
            return Ok(blogs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BlogDTO>> GetById(int id)
        {
            var blog = await _service.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult<BlogDTO>> Create(BlogDTO blogDto)
        {
            var nuevoBlog = await _service.AddAsync(blogDto);
            return Ok(nuevoBlog);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BlogDTO blogDto)
        {
            await _service.UpdateAsync(blogDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _service.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}