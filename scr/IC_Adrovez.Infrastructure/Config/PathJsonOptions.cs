using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC_Adrovez.Infrastructure.Config
{
    public sealed class PathJsonOptions
    {
        public const string SectionName = "PathDataJson";

        /// <summary>
        /// Ruta del archivo JSON (ej: "Data/JsonEjemplo.json" o ruta absoluta).
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }
}
