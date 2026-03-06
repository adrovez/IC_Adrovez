namespace IC_Adrovez.Application.DTOs
{
    public sealed record FacturasPorComunaDto(
    decimal Comuna,
    IReadOnlyList<FacturaDto> Facturas,
    decimal MontoTotalComuna
    );
}
