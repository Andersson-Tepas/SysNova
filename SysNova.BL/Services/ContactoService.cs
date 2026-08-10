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
    public class ContactoService : IContactoService
    {
        private readonly IContactoRepository _repository;
        private readonly IMapper _mapper;

        public ContactoService(IContactoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactoDTO>> GetAllAsync()
        {
            var contactos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactoDTO>>(contactos);
        }

        public async Task<ContactoDTO?> GetByIdAsync(int id)
        {
            var contacto = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactoDTO?>(contacto);
        }

        public async Task<IEnumerable<ContactoDTO>> FindAsync(
            Expression<Func<ContactoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Contacto, bool>>>(predicate);
            var contactos = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<ContactoDTO>>(contactos);
        }

        public async Task<ContactoDTO> AddAsync(ContactoDTO contactoDto)
        {
            var entity = _mapper.Map<Contacto>(contactoDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ContactoDTO>(result);
        }

        public async Task UpdateAsync(ContactoDTO contactoDto)
        {
            var entity = _mapper.Map<Contacto>(contactoDto);
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
            Expression<Func<ContactoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Contacto, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}