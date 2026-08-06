using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Banner : BaseEntity
    {
        public int BannerId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string? SubTitulo { get; set; }

        public string Imagen { get; set; } = string.Empty;

        public string? BotonTexto { get; set; }

        public string? BotonUrl { get; set; }

        public int Orden { get; set; }

        public bool Mostrar { get; set; } = true;
    }
}
