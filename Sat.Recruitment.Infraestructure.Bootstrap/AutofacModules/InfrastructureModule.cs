using Autofac;
using Microsoft.Extensions.Configuration;

namespace Sat.Recruitment.Infrastructure.Bootstrap.AutofacModules
{
    public class InfrastructureModule : Module
    {
        private readonly IConfiguration configuration;

        public InfrastructureModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
                        
        }        
    }
}
