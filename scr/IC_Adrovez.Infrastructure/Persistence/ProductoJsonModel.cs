using System.Text.Json.Serialization;

namespace IC_Adrovez.Infrastructure.Persistence
{
    internal class ProductoJsonModel
    {
        public string Descripcion { get; set; } = string.Empty;

        // En el JSON se llama "Valor". En Domain lo modelamos como "Precio".
        [JsonPropertyName("Valor")]
        public decimal Precio { get; set; }
    }
}
