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
    public class FavoritoService : IFavoritoService
    {
        private readonly IFavoritoRepository _repository;
        private readonly IMapper _mapper;

        public FavoritoService(IFavoritoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavoritoDTO>> GetAllAsync()
        {
            var favoritos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<FavoritoDTO>>(favoritos);
        }

        public async Task<FavoritoDTO?> GetByIdAsync(int id)
        {
            var favorito = await _repository.GetByIdAsync(id);
            return _mapper.Map<FavoritoDTO?>(favorito);
        }

        public async Task<IEnumerable<FavoritoDTO>> FindAsync(
            Expression<Func<FavoritoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Favorito, bool>>>(predicate);
            var favoritos = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<FavoritoDTO>>(favoritos);
        }

        public async Task<FavoritoDTO> AddAsync(FavoritoDTO favoritoDto)
        {
            var entity = _mapper.Map<Favorito>(favoritoDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<FavoritoDTO>(result);
        }

        public async Task UpdateAsync(FavoritoDTO favoritoDto)
        {
            var entity = _mapper.Map<Favorito>(favoritoDto);
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
            Expression<Func<FavoritoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Favorito, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}