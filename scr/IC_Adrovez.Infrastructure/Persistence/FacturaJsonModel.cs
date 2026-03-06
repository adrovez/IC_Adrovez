namespace IC_Adrovez.Infrastructure.Persistence
{
    internal class FacturaJsonModel
    {
        public decimal NumeroDocumento { get; set; }

        public decimal RUTVendedor { get; set; }
        public string DvVendedor { get; set; } = string.Empty;

        public decimal RUTComprador { get; set; }
        public string DvComprador { get; set; } = string.Empty;

        public string DireccionComprador { get; set; } = string.Empty;
        public decimal ComunaComprador { get; set; }
        public decimal RegionComprador { get; set; }

        // En el JSON viene, pero en tu Domain lo calculas desde el detalle.
        public decimal TotalFactura { get; set; }

        public List<DetalleFacturaJsonModel> DetalleFactura { get; set; } = new();
    }
}
