using IK.SCP.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IK.SCP.Infrastructure
{
    public static class DependencyInjection
	{
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Context>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}

