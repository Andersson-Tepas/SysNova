using System;
using SysNova.EN.Enums;

namespace SysNova.DTO
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }

        public string NumeroPedido { get; set; } = string.Empty;

        public int ClienteId { get; set; }

        public int MetodoPagoId { get; set; }

        public DateTime FechaPedido { get; set; }

        public decimal SubTotal { get; set; }

        public decimal IVA { get; set; }

        public decimal Descuento { get; set; }

        public decimal Total { get; set; }

        public EstadoPedido Estado { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}