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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompradorConMasComprasDto>> GetCompradorTop(CancellationToken cancellationToken = default)
        {
            CompradorConMasComprasDto? result;

            result = await _facturaService.GetTopCompradorAsync(cancellationToken);

            if (result is null)
                return NotFound(new ProblemDetails {
                    Title = "Recurso no encontrado",
                    Detail = "No existen compras registradas.",
                    Status = StatusCodes.Status404NotFound
                });

            return Ok(result);
        }

   
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
