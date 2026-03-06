using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC_Adrovez.Domain.Entities
{
    public  class Factura
    {
        private readonly List<DetalleFactura> _detalle = new();

        public int NumeroDocumento { get; }

        // Mantengo vendedor/comprador simple (sin PersonaTributaria)
        public int RutVendedor { get; }
        public string DvVendedor { get; }

        public int RutComprador { get; }
        public string DvComprador { get; }

        public string DireccionComprador { get; }
        public int ComunaComprador { get; }
        public int RegionComprador { get; }

        public IReadOnlyCollection<DetalleFactura> DetalleFacturas => _detalle.AsReadOnly();

        // TotalFactura = sumatoria de TotalProducto (detalle)
        public decimal TotalFactura => _detalle.Sum(d => d.TotalProducto);

        public Factura(
            int numeroDocumento,
            int rutVendedor,
            string dvVendedor,
            int rutComprador,
            string dvComprador,
            string direccionComprador,
            int comunaComprador,
            int regionComprador,
            IEnumerable<DetalleFactura> detalleFactura)
        {
            if (numeroDocumento <= 0)
                throw new ArgumentException("El número de documento debe ser > 0.", nameof(numeroDocumento));

            if (rutVendedor <= 0) throw new ArgumentException("El RUT del vendedor debe ser > 0.", nameof(rutVendedor));
            if (string.IsNullOrWhiteSpace(dvVendedor)) throw new ArgumentException("DV del vendedor es obligatorio.", nameof(dvVendedor));

            if (rutComprador <= 0) throw new ArgumentException("El RUT del comprador debe ser > 0.", nameof(rutComprador));
            if (string.IsNullOrWhiteSpace(dvComprador)) throw new ArgumentException("DV del comprador es obligatorio.", nameof(dvComprador));

            if (string.IsNullOrWhiteSpace(direccionComprador))
                throw new ArgumentException("La dirección del comprador es obligatoria.", nameof(direccionComprador));

            if (comunaComprador <= 0)
                throw new ArgumentException("La comuna del comprador debe ser > 0.", nameof(comunaComprador));

            if (regionComprador <= 0)
                throw new ArgumentException("La región del comprador debe ser > 0.", nameof(regionComprador));

            if (detalleFactura is null)
                throw new ArgumentNullException(nameof(detalleFactura));

            NumeroDocumento = numeroDocumento;
            RutVendedor = rutVendedor;
            DvVendedor = dvVendedor.Trim();
            RutComprador = rutComprador;
            DvComprador = dvComprador.Trim();
            DireccionComprador = direccionComprador.Trim();
            ComunaComprador = comunaComprador;
            RegionComprador = regionComprador;

            _detalle.AddRange(detalleFactura);

            if (_detalle.Count == 0)
                throw new ArgumentException("La factura debe tener al menos un detalle.", nameof(detalleFactura));
        }
    }
}
