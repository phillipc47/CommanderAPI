using Commander.Data;
using Commander.Data.SQL;
using Microsoft.Extensions.DependencyInjection;

namespace Commander.Initializers
{
	public static class RepositoryInitializer
	{
		public static void Configure(IServiceCollection services)
		{
			//services.AddScoped<ICommanderRepository, HardCodedCommanderRepository>();
			services.AddScoped<ICommanderRepository, SqlServerCommanderRepository>();
		}
	}
}
