using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.DAL.Context;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;

namespace SysNova.Repository.Repositories
{
    public class PreguntaFrecuenteRepository : Repository<PreguntaFrecuente>, IPreguntaFrecuenteRepository
    {
        public PreguntaFrecuenteRepository(SysNovaDbContext context)
            : base(context)
        {
        }
    }
}
