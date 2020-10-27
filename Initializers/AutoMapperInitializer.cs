using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Commander.Initializers
{
	public static class AutoMapperInitializer
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}
	}
}
