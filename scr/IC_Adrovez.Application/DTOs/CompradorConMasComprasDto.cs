namespace IC_Adrovez.Application.DTOs
{
    public sealed record CompradorConMasComprasDto(
    string RutComprador,
    decimal CantidadFacturas,
    decimal MontoTotalCompras,
    string Criterio // "CantidadFacturas" o "MontoTotal"
    );
}
