using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.BL.Interfaces;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;
using System.Linq.Expressions;

namespace SysNova.BL.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pedido?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Pedido>> FindAsync(
            Expression<Func<Pedido, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Pedido> AddAsync(Pedido pedido)
        {
            return await _repository.AddAsync(pedido);
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            await _repository.UpdateAsync(pedido);
        }

        public async Task DeleteAsync(Pedido pedido)
        {
            await _repository.DeleteAsync(pedido);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Pedido, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}