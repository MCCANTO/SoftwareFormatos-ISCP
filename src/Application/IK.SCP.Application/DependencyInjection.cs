using IK.SCP.Externo;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IK.SCP.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());

			//services.AddScoped<ISoftEmpAcceso, SoftEmpAccesoClient>();
			//services.AddScoped<ISeguridad, Seguridad>();

			services.AddScoped<IAutorizacionService, AutorizacionService>();

            return services;
		}
	}
}

