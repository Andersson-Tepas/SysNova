using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.EN.Enums;

namespace SysNova.EN.Entities
{
    public class Envio : BaseEntity
    {
        public int EnvioId { get; set; }

        public int PedidoId { get; set; }

        public string EmpresaTransportista { get; set; } = string.Empty;

        public string NumeroGuia { get; set; } = string.Empty;

        public EstadoEnvio Estado { get; set; } = EstadoEnvio.Pendiente;

        public DateTime? FechaSalida { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public virtual Pedido Pedido { get; set; } = null!;
    }
}
