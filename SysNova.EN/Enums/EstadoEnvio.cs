using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Enums
{
    public enum EstadoEnvio
    {
        Pendiente = 1,
        EnPreparacion = 2,
        EnRuta = 3,
        Entregado = 4,
        Cancelado = 5
    }
}
