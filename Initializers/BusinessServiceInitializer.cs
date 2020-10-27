using Commander.Services.Category;
using Commander.Services.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Commander.Initializers
{
	public static class BusinessServiceInitializer
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddScoped<ICommandService, CommandService>();
			services.AddScoped<ICategoryService, CategoryService>();
		}
	}
}
