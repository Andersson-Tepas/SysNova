using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Resena : BaseEntity
    {
        public int ResenaId { get; set; }

        public int ClienteId { get; set; }

        public int ProductoId { get; set; }

        public byte Calificacion { get; set; }

        public string Comentario { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        public virtual Cliente Cliente { get; set; } = null!;

        public virtual Producto Producto { get; set; } = null!;
    }
}
