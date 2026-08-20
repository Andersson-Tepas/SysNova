using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using SysNova.BL.Interfaces;
using SysNova.DTO;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;

namespace SysNova.BL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDTO>> GetAllAsync()
        {
            var blogs = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }

        public async Task<BlogDTO?> GetByIdAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            return _mapper.Map<BlogDTO?>(blog);
        }

        public async Task<IEnumerable<BlogDTO>> FindAsync(
            Expression<Func<BlogDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Blog, bool>>>(predicate);
            var blogs = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }

        public async Task<BlogDTO> AddAsync(BlogDTO blogDto)
        {
            var entity = _mapper.Map<Blog>(blogDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<BlogDTO>(result);
        }

        public async Task UpdateAsync(BlogDTO blogDto)
        {
            var entity = await _repository.GetByIdAsync(blogDto.BlogId);

            if (entity == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el blog con ID {blogDto.BlogId}.");
            }

            entity.Titulo = blogDto.Titulo;
            entity.Resumen = blogDto.Resumen;
            entity.Contenido = blogDto.Contenido;

            entity.Imagen = blogDto.Imagen;

            entity.Autor = blogDto.Autor;
            entity.FechaPublicacion = blogDto.FechaPublicacion;
            entity.Visitas = blogDto.Visitas;

            entity.Activo = blogDto.Activo;
            entity.FechaModificacion = DateTime.Now;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<BlogDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Blog, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}