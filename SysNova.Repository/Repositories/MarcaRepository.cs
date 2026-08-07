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
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        public MarcaRepository(SysNovaDbContext context)
            : base(context)
        {
        }
    }
}
