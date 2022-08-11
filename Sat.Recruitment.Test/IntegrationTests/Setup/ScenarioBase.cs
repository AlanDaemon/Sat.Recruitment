using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace Sat.Recruitment.Test.IntegrationTests.Setup
{
    public class ScenarioBase
    {
        /*
         * Before run test, set the environment variable ASPNETCORE_ENVIRONMENT
         */
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(ScenarioBase)).Location;
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var env = config.GetValue<string>("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var host = Host.CreateDefaultBuilder()
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureWebHostDefaults(webHostBuilder =>
             {
                 webHostBuilder
                     .UseTestServer()
                     .UseEnvironment(env)
                     .UseContentRoot(Path.GetDirectoryName(path))
                     .UseConfiguration(config)
                     .UseStartup<TestsStartUp>();
             })
             .Build();

            host.Start();
            return host.GetTestServer();
        }       
    }
}

