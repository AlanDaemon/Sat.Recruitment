using Autofac;
using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Infrastructure.Bootstrap.AutofacModules;
using Sat.Recruitment.Infrastructure.Bootstrap.AutofacModules.Features;

namespace Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class AutofacConfigurationServiceCollectionExtensions
    {
        public static void AddConfigurationAutofac(this ContainerBuilder builder, IConfiguration configuration)
        {          
            builder.RegisterModule<UserModule>();            
            
            builder.RegisterModule(new InfrastructureModule(configuration));
            builder.RegisterModule(new MediatorModule(configuration.GetValue("CommandLoggingEnabled", false)));                   
        }
    }
}
