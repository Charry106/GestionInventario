
using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using System;
using System.Collections.Generic;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class MovimientoServicio : IMovimientoServicio
    {
        private readonly IMovimientoRepositorio _movimientoRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;

        public MovimientoServicio(IMovimientoRepositorio movimientoRepositorio, IProductoRepositorio productoRepositorio)
        {
            _movimientoRepositorio = movimientoRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        public void RegistrarMovimiento(int productoId, string tipoMovimiento, int cantidad, string motivo)
        {
            // Obtener el producto asociado al movimiento
            var producto = _productoRepositorio.ObtenerProducto(productoId);
            if (producto == null)
            {
                throw new Exception("Producto no encontrado");
            }

            // Ajustar la cantidad disponible en función del tipo de movimiento
            if (tipoMovimiento.ToLower() == "entrada")
            {
                producto.CantidadDisponible += cantidad;
            }
            else if (tipoMovimiento.ToLower() == "salida")
            {
                if (producto.CantidadDisponible < cantidad)
                {
                    throw new Exception("Cantidad insuficiente en inventario");
                }
                producto.CantidadDisponible -= cantidad;
            }
            else
            {
                throw new Exception("Tipo de movimiento no válido");
            }

            // Actualizar la cantidad disponible en la tabla Producto
            _productoRepositorio.ModificarProducto(producto);

            // Crear el registro de movimiento en la tabla Movimiento
            var movimiento = new Movimiento
            {
                ProductoId = productoId,
                TipoMovimiento = tipoMovimiento,
                Cantidad = cantidad,
                Motivo = motivo,
                Fecha = DateTime.Now
            };
            _movimientoRepositorio.RegistrarMovimiento(movimiento);
        }

        public Movimiento ObtenerMovimiento(int id)
        {
            return _movimientoRepositorio.ObtenerMovimiento(id);
        }

        public List<Movimiento> ObtenerMovimientosPorProducto(int productoId)
        {
            return _movimientoRepositorio.ObtenerMovimientosPorProducto(productoId);
        }

        public List<Movimiento> ObtenerTodos()
        {
            return _movimientoRepositorio.ObtenerTodos();
        }
    }
}


