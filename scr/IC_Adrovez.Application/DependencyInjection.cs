using IC_Adrovez.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IC_Adrovez.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IFacturaService, FacturaService>();
            return services;
        }
    }
}
