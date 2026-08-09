using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<Blog>>> GetAll()
        {
            var blogs = await _service.GetAllAsync();

            return Ok(blogs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Blog>> GetById(int id)
        {
            var blog = await _service.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> Create(Blog blog)
        {
            var nuevoBlog = await _service.AddAsync(blog);

            return Ok(nuevoBlog);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Blog blog)
        {
            await _service.UpdateAsync(blog);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _service.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            await _service.DeleteAsync(blog);

            return NoContent();
        }
    }
}