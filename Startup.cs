using Commander.Data.SQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Commander.Initializers;

namespace Commander
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			SwaggerInitializer.Configure(services);

			SetupCommanderDatabase(services);

			services.AddControllers().AddNewtonsoftJson(serializer => serializer.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

			AutoMapperInitializer.Configure(services);

			RepositoryInitializer.Configure(services);

			BusinessServiceInitializer.Configure(services);

			DALInitializer.Configure(services);
		}

		private void SetupCommanderDatabase(IServiceCollection services)
		{
			var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("CommanderConnection"));
			builder.Password = Configuration["CommanderDatabasePassword"];

			services.AddDbContext<CommanderContext>(
				options => options.UseLazyLoadingProxies()
					.UseSqlServer(builder.ConnectionString));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			SwaggerInitializer.Configure(app);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
