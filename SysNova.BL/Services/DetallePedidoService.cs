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
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IDetallePedidoRepository _repository;

        public DetallePedidoService(IDetallePedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DetallePedido>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DetallePedido?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<DetallePedido>> FindAsync(
            Expression<Func<DetallePedido, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<DetallePedido> AddAsync(DetallePedido detalle)
        {
            return await _repository.AddAsync(detalle);
        }

        public async Task UpdateAsync(DetallePedido detalle)
        {
            await _repository.UpdateAsync(detalle);
        }

        public async Task DeleteAsync(DetallePedido detalle)
        {
            await _repository.DeleteAsync(detalle);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<DetallePedido, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
