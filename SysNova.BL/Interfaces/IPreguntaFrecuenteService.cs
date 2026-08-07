using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IPreguntaFrecuenteService
    {
        Task<IEnumerable<PreguntaFrecuente>> GetAllAsync();
        Task<PreguntaFrecuente?> GetByIdAsync(object id);
        Task<IEnumerable<PreguntaFrecuente>> FindAsync(Expression<Func<PreguntaFrecuente, bool>> predicate);
        Task<PreguntaFrecuente> AddAsync(PreguntaFrecuente preguntaFrecuente);
        Task UpdateAsync(PreguntaFrecuente preguntaFrecuente);
        Task DeleteAsync(PreguntaFrecuente preguntaFrecuente);
        Task<bool> ExistsAsync(Expression<Func<PreguntaFrecuente, bool>> predicate);
    }

}