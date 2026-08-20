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
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _repository;
        private readonly IMapper _mapper;

        public BannerService(IBannerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BannerDTO>> GetAllAsync()
        {
            var banners = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BannerDTO>>(banners);
        }

        public async Task<BannerDTO?> GetByIdAsync(int id)
        {
            var banner = await _repository.GetByIdAsync(id);
            return _mapper.Map<BannerDTO?>(banner);
        }

        public async Task<IEnumerable<BannerDTO>> FindAsync(
            Expression<Func<BannerDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Banner, bool>>>(predicate);
            var banners = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<BannerDTO>>(banners);
        }

        public async Task<BannerDTO> AddAsync(BannerDTO bannerDto)
        {
            var entity = _mapper.Map<Banner>(bannerDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<BannerDTO>(result);
        }

        public async Task UpdateAsync(BannerDTO bannerDto)
        {
            var entity = await _repository.GetByIdAsync(bannerDto.BannerId);

            if (entity == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el banner con ID {bannerDto.BannerId}.");
            }

            entity.Titulo = bannerDto.Titulo;
            entity.SubTitulo = bannerDto.SubTitulo;

            entity.Imagen = bannerDto.Imagen;

            entity.BotonTexto = bannerDto.BotonTexto;
            entity.BotonUrl = bannerDto.BotonUrl;

            entity.Orden = bannerDto.Orden;
            entity.Mostrar = bannerDto.Mostrar;
            entity.Activo = bannerDto.Activo;

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
            Expression<Func<BannerDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Banner, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}