using SysNova.DTO;

namespace SysNova.Web.Services
{
    public class CarritoService
    {
        private readonly List<CarritoItem> _items = new();


        public IReadOnlyList<CarritoItem> Items =>
            _items.AsReadOnly();


        // ==========================================
        // EVENTOS
        // ==========================================

        public event Action? OnChange;

        public event Action? OnAbrirCarrito;


        // ==========================================
        // CANTIDAD TOTAL
        // ==========================================

        public int CantidadTotal =>
            _items.Sum(x => x.Cantidad);


        // ==========================================
        // TOTAL
        // ==========================================

        public decimal Total =>
            _items.Sum(
                x => x.Producto.Precio *
                     x.Cantidad);


        // ==========================================
        // AGREGAR
        // ==========================================

        public void AgregarProducto(
            ProductoDTO producto,
            int cantidad = 1)
        {
            if (producto == null)
                return;


            if (producto.Stock <= 0)
                return;


            if (cantidad <= 0)
                cantidad = 1;


            var existente =
                _items.FirstOrDefault(
                    x =>
                        x.Producto.ProductoId ==
                        producto.ProductoId);


            if (existente != null)
            {
                var nuevaCantidad =
                    existente.Cantidad +
                    cantidad;


                existente.Cantidad =
                    Math.Min(
                        nuevaCantidad,
                        producto.Stock);
            }


            if (existente == null)
            {
                _items.Add(
                    new CarritoItem
                    {
                        Producto =
                            producto,

                        Cantidad =
                            Math.Min(
                                cantidad,
                                producto.Stock)
                    });
            }


            NotificarCambio();
        }


        // ==========================================
        // ACTUALIZAR CANTIDAD
        // ==========================================

        public void ActualizarCantidad(
            int productoId,
            int nuevaCantidad)
        {
            var item =
                _items.FirstOrDefault(
                    x =>
                        x.Producto.ProductoId ==
                        productoId);


            if (item == null)
                return;


            if (nuevaCantidad <= 0)
            {
                EliminarProducto(
                    productoId);

                return;
            }


            item.Cantidad =
                Math.Min(
                    nuevaCantidad,
                    item.Producto.Stock);


            NotificarCambio();
        }


        // ==========================================
        // ELIMINAR
        // ==========================================

        public void EliminarProducto(
            int productoId)
        {
            var item =
                _items.FirstOrDefault(
                    x =>
                        x.Producto.ProductoId ==
                        productoId);


            if (item == null)
                return;


            _items.Remove(item);


            NotificarCambio();
        }


        // ==========================================
        // VACIAR
        // ==========================================

        public void Vaciar()
        {
            _items.Clear();


            NotificarCambio();
        }


        // ==========================================
        // SOLICITAR ABRIR CARRITO
        // ==========================================

        public void SolicitarAbrirCarrito()
        {
            OnAbrirCarrito?.Invoke();
        }


        // ==========================================
        // NOTIFICAR
        // ==========================================

        private void NotificarCambio()
        {
            OnChange?.Invoke();
        }
    }


    // ==============================================
    // ITEM
    // ==============================================

    public class CarritoItem
    {
        public ProductoDTO Producto { get; set; }
            = new();


        public int Cantidad { get; set; }


        public decimal SubTotal =>
            Producto.Precio *
            Cantidad;
    }
}