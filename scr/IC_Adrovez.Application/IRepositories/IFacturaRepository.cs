using IC_Adrovez.Domain.Entities;

namespace IC_Adrovez.Application.IRepositories
{
    public interface IFacturaRepository
    {
        Task<IReadOnlyList<Factura>> GetAllAsync(CancellationToken ct = default);
    }
}
