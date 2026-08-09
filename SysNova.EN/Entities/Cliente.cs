using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Cliente : BaseEntity
    {
        public int ClienteId { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Departamento { get; set; }

        public string? Municipio { get; set; }

        public virtual ICollection<Carrito>? Carritos { get; set; } 

        public virtual ICollection<Pedido>? Pedidos { get; set; } 

        public virtual ICollection<Favorito>? Favoritos { get; set; } 

        public virtual ICollection<Resena>? Resenas { get; set; } 
    }
}
