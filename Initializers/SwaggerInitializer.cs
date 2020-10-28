using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Commander.Initializers
{
	public static class SwaggerInitializer
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo 
				{
					Version = "v1", 
					Title = "Commander API", 
					Description = "A very simple .NET Core API to store and retrieve common commands",
					Contact = new OpenApiContact
					{
						Name = "Phillip Casey",
						Email = "noreply@gmail.com",
					},
					License = new OpenApiLicense
					{
						Name = "Sample License for demonstration purposes"
					}
				});
				options.EnableAnnotations();
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
