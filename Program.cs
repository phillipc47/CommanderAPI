using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace Commander
{
#pragma warning disable CS1591
   public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

            builder.ConfigureAppConfiguration((hostContext, builder) =>
                {
                    if( hostContext.HostingEnvironment.IsDevelopment() )
                    {
                        builder.AddUserSecrets<Program>();
                    }
                });

            return builder;
        }
    }
#pragma warning restore CS1591
}
