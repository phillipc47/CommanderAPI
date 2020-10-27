using Commander.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace Commander.Initializers
{
	public static class DALInitializer
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddScoped<ICommandDataAccessLayer, CommandDAL>();
		}
	}
}
