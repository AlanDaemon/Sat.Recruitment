using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Sat.Recruitment.Api
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                        .UseSerilog((hostingContext, loggerConfiguration) =>
                        {
                            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                        })
                       .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                       .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.ConfigureKestrel(serverOptions =>
                            {
                                serverOptions.AddServerHeader = false;
                            })
                        .UseStartup<Startup>();
                        });
        }
    }
}
