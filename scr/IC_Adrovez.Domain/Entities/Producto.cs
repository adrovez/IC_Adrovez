namespace IC_Adrovez.Domain.Entities
{
    public class Producto
    {
        public string Descripcion { get; }
        public decimal Precio { get; } // Precio unitario

        public Producto(string descripcion, decimal precio)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción del producto es obligatoria.", nameof(descripcion));

            if (precio < 0)
                throw new ArgumentException("El precio del producto debe ser >= 0.", nameof(precio));

            Descripcion = descripcion.Trim();
            Precio = precio;
        }
    }
}
