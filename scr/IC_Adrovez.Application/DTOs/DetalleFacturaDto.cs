namespace IC_Adrovez.Application.DTOs
{
    public sealed record DetalleFacturaDto(
    decimal CantidadProducto,
    ProductoDto Producto,
    decimal TotalProducto
);
}
