using IC_Adrovez.Application.DTOs;

namespace IC_Adrovez.Application.Services
{
    public interface IFacturaService
    {
        Task<IReadOnlyList<FacturaDto>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<FacturaDto>> GetCompradorPorRutAsync(string rutComprador, CancellationToken ct = default);
        Task<CompradorConMasComprasDto?> GetTopCompradorAsync(CancellationToken ct = default);
        Task<IReadOnlyList<CompradorResumenDto>> GetCompradorMontoTotalAsync(CancellationToken ct = default);
        Task<IReadOnlyList<FacturasPorComunaDto>> GetFacturasPorComunaAsync(CancellationToken ct = default);
        Task<FacturasPorComunaDto?> GetFacturasPorComunaAsync(int comuna, CancellationToken ct = default);

    }
}
