using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Commander.Initializers
{
	public static class SwaggerInitializer
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Commander API", Version = "v1" });
			}
			);
		}

		public static void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseSwagger();

			applicationBuilder.UseSwaggerUI(options => 
				{ 
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "Commander API V1");
					options.RoutePrefix = "help";
				});
		}
	}
}
