using IC_Adrovez.Application.DTOs;
using IC_Adrovez.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IC_Adrovez.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompradoresController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public CompradoresController(IFacturaService facturaService)
        {
            _facturaService = facturaService ?? throw new ArgumentNullException(nameof(facturaService));
        }

        
        [HttpGet("top")]
        [ProducesResponseType(typeof(CompradorConMasComprasDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompradorConMasComprasDto>> GetCompradorTop(CancellationToken cancellationToken = default)
        {
            CompradorConMasComprasDto? result;

            result = await _facturaService.GetTopCompradorAsync(cancellationToken);

            if (result is null)
                return NotFound("No existen compras registradas.");

            return Ok(result);
        }


        /// <summary>
        /// Retorna la lista de compradores con el monto total de compras realizadas.
        /// </summary>
        [HttpGet("totales")]
        [ProducesResponseType(typeof(IReadOnlyList<CompradorResumenDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<CompradorResumenDto>>> GetCompradoresMontoTotal(
            CancellationToken cancellationToken)
        {
            var result = await _facturaService.GetCompradorMontoTotalAsync(cancellationToken);
            return Ok(result);
        }

    }
}
