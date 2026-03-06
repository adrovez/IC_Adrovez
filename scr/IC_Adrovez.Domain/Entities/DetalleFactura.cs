using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC_Adrovez.Domain.Entities
{
    public class DetalleFactura
    {
        public decimal CantidadProducto { get; }
        public Producto Producto { get; }
        public decimal TotalProducto { get; } // viene en el JSON

        public DetalleFactura(decimal cantidadProducto, Producto producto, decimal totalProducto)
        {
            if (cantidadProducto < 0)
                throw new ArgumentException("La cantidad de producto debe ser >= 0.", nameof(cantidadProducto));

            Producto = producto ?? throw new ArgumentNullException(nameof(producto));

            if (totalProducto < 0)
                throw new ArgumentException("El total del producto debe ser >= 0.", nameof(totalProducto));

            CantidadProducto = cantidadProducto;
            TotalProducto = totalProducto;
        }

    }
}
