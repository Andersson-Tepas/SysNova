using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Marca : BaseEntity
    {
        public int MarcaId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string? Logo { get; set; }

        public string? Pais { get; set; }

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
