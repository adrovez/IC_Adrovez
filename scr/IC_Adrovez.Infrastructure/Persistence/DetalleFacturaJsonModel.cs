namespace IC_Adrovez.Infrastructure.Persistence
{
    internal class DetalleFacturaJsonModel
    {
        public decimal CantidadProducto { get; set; }
        public ProductoJsonModel Producto { get; set; } = new();
        public decimal TotalProducto { get; set; }
    }
}
