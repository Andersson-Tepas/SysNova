using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class FavoritoDTO
    {
        public int FavoritoId { get; set; }

        public int ClienteId { get; set; }

        public int ProductoId { get; set; }

        public DateTime FechaAgregado { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
