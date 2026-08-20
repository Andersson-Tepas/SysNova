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
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task<CategoriaDTO?> GetByIdAsync(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoriaDTO?>(categoria);
        }

        public async Task<IEnumerable<CategoriaDTO>> FindAsync(
            Expression<Func<CategoriaDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Categoria, bool>>>(predicate);
            var categorias = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task<CategoriaDTO> AddAsync(CategoriaDTO categoriaDto)
        {
            var entity = _mapper.Map<Categoria>(categoriaDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<CategoriaDTO>(result);
        }

        public async Task UpdateAsync(CategoriaDTO categoriaDto)
        {
            var entity = await _repository.GetByIdAsync(categoriaDto.CategoriaId);

            if (entity == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró la categoría con ID {categoriaDto.CategoriaId}.");
            }

            entity.Nombre = categoriaDto.Nombre;
            entity.Descripcion = categoriaDto.Descripcion;
            entity.Icono = categoriaDto.Icono;

            entity.Imagen = categoriaDto.Imagen;

            entity.Activo = categoriaDto.Activo;

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
            Expression<Func<CategoriaDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Categoria, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}