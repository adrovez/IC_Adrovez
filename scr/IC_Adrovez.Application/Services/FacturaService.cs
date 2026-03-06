using IC_Adrovez.Application.DTOs;
using IC_Adrovez.Application.IRepositories;
using IC_Adrovez.Domain.Entities;

namespace IC_Adrovez.Application.Services
{
    public sealed class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repo;

        public FacturaService(IFacturaRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // 1) Lista completa + total calculado (Domain.TotalFactura ya lo calcula)
        public async Task<IReadOnlyList<FacturaDto>> GetAllAsync(CancellationToken ct = default)
        {
            var facturas = await _repo.GetAllAsync(ct);
            return facturas.Select(MapToDto).ToList().AsReadOnly();
        }

        // 2) Facturas por RUT comprador 
        public async Task<IReadOnlyList<FacturaDto>> GetCompradorPorRutAsync(decimal rutComprador, CancellationToken ct = default)
        {
           
            var facturas = await _repo.GetAllAsync(ct);

            var filtered = facturas
                .Where(f => f.RutComprador == rutComprador)
                .Select(MapToDto)
                .ToList()
                .AsReadOnly();

            return filtered;
        }

        // 3) Comprador con más compras
        //    Variante A: por cantidad de facturas
        public async Task<CompradorConMasComprasDto?> GetTopCompradorAsync(CancellationToken ct = default)
        {
            var facturas = await _repo.GetAllAsync(ct);

            var group = facturas
                .GroupBy(f => $"{f.RutComprador}-{f.DvComprador}")
                .Select(g => new
                {
                    Rut = g.Key,
                    Cantidad = g.Count(),
                    Monto = g.Sum(x => x.TotalFactura)
                })
                .OrderByDescending(x => x.Cantidad)
                .ThenByDescending(x => x.Monto)
                .FirstOrDefault();

            if (group is null) return null;

            return new CompradorConMasComprasDto(
                RutComprador: group.Rut,
                CantidadFacturas: group.Cantidad,
                MontoTotalCompras: group.Monto,
                Criterio: "CantidadFacturas"
            );
        }


        // 4) Lista de compradores con monto total de compras realizadas
        public async Task<IReadOnlyList<CompradorResumenDto>> GetCompradorMontoTotalAsync(CancellationToken ct = default)
        {
            var facturas = await _repo.GetAllAsync(ct);

            var result = facturas
                .GroupBy(f => $"{f.RutComprador}-{f.DvComprador}")
                .Select(g => new CompradorResumenDto(
                    RutComprador: g.Key,
                    CantidadFacturas: g.Count(),
                    MontoTotalCompras: g.Sum(x => x.TotalFactura)
                ))
                .OrderByDescending(x => x.MontoTotalCompras)
                .ToList()
                .AsReadOnly();

            return result;
        }

        // 5) Facturas agrupadas por comuna (todas)
        public async Task<IReadOnlyList<FacturasPorComunaDto>> GetFacturasPorComunaAsync(CancellationToken ct = default)
        {
            var facturas = await _repo.GetAllAsync(ct);

            var result = facturas
                .GroupBy(f => f.ComunaComprador)
                .OrderBy(g => g.Key)
                .Select(g =>
                {
                    var facturasDto = g.Select(MapToDto).ToList().AsReadOnly();
                    var monto = g.Sum(x => x.TotalFactura);

                    return new FacturasPorComunaDto(
                        Comuna: g.Key,
                        Facturas: facturasDto,
                        MontoTotalComuna: monto
                    );
                })
                .ToList()
                .AsReadOnly();

            return result;
        }

        // 5b) Facturas de una comuna específica
        public async Task<FacturasPorComunaDto?> GetFacturasPorComunaAsync(int comuna, CancellationToken ct = default)
        {
            var facturas = await _repo.GetAllAsync(ct);

            var group = facturas
                .Where(f => f.ComunaComprador == comuna)
                .ToList();

            if (group.Count == 0) return null;

            var facturasDto = group.Select(MapToDto).ToList().AsReadOnly();
            var monto = group.Sum(x => x.TotalFactura);

            return new FacturasPorComunaDto(
                Comuna: comuna,
                Facturas: facturasDto,
                MontoTotalComuna: monto
            );
        }

        #region ============================= Helpers / Mapping =============================
        private static FacturaDto MapToDto(Factura f)
        {
            var detalle = f.DetalleFacturas.Select(d =>
                new DetalleFacturaDto(
                    CantidadProducto: d.CantidadProducto,
                    Producto: new ProductoDto(d.Producto.Descripcion, d.Producto.Precio),
                    TotalProducto: d.TotalProducto
                )).ToList().AsReadOnly();

            return new FacturaDto(
                NumeroDocumento: f.NumeroDocumento,
                RutVendedor: $"{f.RutVendedor}-{f.DvVendedor}",
                RutComprador: $"{f.RutComprador}-{f.DvComprador}",
                DireccionComprador: f.DireccionComprador,
                ComunaComprador: f.ComunaComprador,
                RegionComprador: f.RegionComprador,
                TotalFactura: f.TotalFactura,
                DetalleFactura: detalle
            );
        }

        private static string NormalizeRut(string rut)
        {
            if (string.IsNullOrWhiteSpace(rut)) return string.Empty;

            return rut.Replace(".", string.Empty)
                      .Replace("-", string.Empty)
                      .Trim()
                      .ToUpperInvariant();
        }

        #endregion

    }
}
