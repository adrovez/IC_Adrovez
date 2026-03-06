using IC_Adrovez.Application.DTOs;
using IC_Adrovez.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IC_Adrovez.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class FacturasController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService ?? throw new ArgumentNullException(nameof(facturaService));
        }

        /// <summary>
        /// Retorna la lista completa de facturas con el total calculado.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<FacturaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<FacturaDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _facturaService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Retorna las facturas de un comprador según su RUT.
        /// Ejemplo: 12345678-9
        /// </summary>
        [HttpGet("comprador/{rut}")]
        [ProducesResponseType(typeof(IReadOnlyList<FacturaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<FacturaDto>>> GetCompradorPorRut(
            string rut,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(rut))
                return BadRequest("El rut es obligatorio.");

            var result = await _facturaService.GetCompradorPorRutAsync(rut, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Retorna las facturas agrupadas por comuna.
        /// </summary>
        [HttpGet("por-comuna")]
        [ProducesResponseType(typeof(IReadOnlyList<FacturasPorComunaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<FacturasPorComunaDto>>> GetGroupedByComuna(
            CancellationToken cancellationToken)
        {
            var result = await _facturaService.GetFacturasPorComunaAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Retorna las facturas de una comuna específica.
        /// </summary>
        [HttpGet("por-comuna/{comuna:int}")]
        [ProducesResponseType(typeof(FacturasPorComunaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FacturasPorComunaDto>> GetByComuna(
            int comuna,
            CancellationToken cancellationToken)
        {
            if (comuna <= 0)
                return BadRequest("La comuna debe ser mayor a cero.");

            var result = await _facturaService.GetFacturasPorComunaAsync(comuna, cancellationToken);

            if (result is null)
                return NotFound($"No se encontraron facturas para la comuna {comuna}.");

            return Ok(result);
        }
    }
}
