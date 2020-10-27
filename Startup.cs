using System;
using AutoMapper;
using Commander.Data;
using Commander.Data.SQL;
using Commander.DataAccessLayer;
using Commander.Services.Category;
using Commander.Services.Command;
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

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			// Repository
			//services.AddScoped<ICommanderRepository, HardCodedCommanderRepository>();
			services.AddScoped<ICommanderRepository, SqlServerCommanderRepository>();

			// Services
			services.AddScoped<ICommandService, CommandService>();
			services.AddScoped<ICategoryService, CategoryService>();

			// DAL
			services.AddScoped<ICommandDataAccessLayer, CommandDAL>();

		}

		private void SetupSwagger(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
				{
					options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Commander API", Version = "v1" });
				}
			);
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
