using IC_Adrovez.Application.IRepositories;
using IC_Adrovez.Infrastructure.Config;
using IC_Adrovez.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IC_Adrovez.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<PathJsonOptions>(config.GetSection(PathJsonOptions.SectionName));
            services.AddScoped<IFacturaRepository, JsonFacturaRepository>();
            return services;
        }
    }
}
