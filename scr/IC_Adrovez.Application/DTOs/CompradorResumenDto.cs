namespace IC_Adrovez.Application.DTOs
{
    public sealed record CompradorResumenDto(
    string RutComprador,
    decimal CantidadFacturas,
    decimal MontoTotalCompras
    );
}
