namespace IC_Adrovez.Application.DTOs
{
    public sealed record FacturaDto(
    decimal NumeroDocumento,
    string RutVendedor,
    string RutComprador,
    string DireccionComprador,
    decimal ComunaComprador,
    decimal RegionComprador,
    decimal TotalFactura,
    IReadOnlyList<DetalleFacturaDto> DetalleFactura
);
}
