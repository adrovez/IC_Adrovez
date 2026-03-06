using IC_Adrovez.Application.IRepositories;
using IC_Adrovez.Domain.Entities;
using IC_Adrovez.Infrastructure.Config;
using IC_Adrovez.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace IC_Adrovez.Infrastructure.Repositories
{
    public sealed class JsonFacturaRepository : IFacturaRepository
    {
        private readonly PathJsonOptions _options;
        public JsonFacturaRepository(IOptions<PathJsonOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IReadOnlyList<Factura>> GetAllAsync(CancellationToken ct = default)
        {

            var path = _options.Path;

            if (string.IsNullOrWhiteSpace(path))
                throw new InvalidOperationException("Path no está configurado.");

            if (!File.Exists(path))
                throw new FileNotFoundException($"No se encontró el archivo JSON en la ruta: {path}", path);

            await using var stream = File.OpenRead(path);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var facturaModels = await JsonSerializer.DeserializeAsync<List<FacturaJsonModel>>(stream, jsonOptions, ct)
                               ?? new List<FacturaJsonModel>();

            var facturas = facturaModels.Select(MapToDomain).ToList().AsReadOnly();

            return facturas;
        }

        private static Factura MapToDomain(FacturaJsonModel m)
        {
            // Map Detalles
            var detalles = (m.DetalleFactura ?? new List<DetalleFacturaJsonModel>())
                .Select(d =>
                {
                    var producto = new Producto(
                        descripcion: d.Producto?.Descripcion ?? string.Empty,
                        precio: d.Producto?.Precio ?? 0m // Domain valida >=0
                    );

                    return new DetalleFactura(
                        cantidadProducto: d.CantidadProducto, // Domain valida >=0
                        producto: producto,
                        totalProducto: d.TotalProducto        // Domain valida >=0
                    );
                })
                .ToList();

            // Crea Factura (TotalFactura se calcula en Domain por sumatoria del detalle)
            return new Factura(
                numeroDocumento: m.NumeroDocumento,
                rutVendedor: m.RUTVendedor,
                dvVendedor: m.DvVendedor,
                rutComprador: m.RUTComprador,
                dvComprador: m.DvComprador,
                direccionComprador: m.DireccionComprador,
                comunaComprador: m.ComunaComprador,
                regionComprador: m.RegionComprador,
                detalleFactura: detalles
            );
        }
    }
}
