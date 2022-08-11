using Autofac;
using Sat.Recruitment.Domain.Features.Users.Repository;
using Sat.Recruitment.Infrastructure.Features.Users.Repository;

namespace Sat.Recruitment.Infrastructure.Bootstrap.AutofacModules.Features
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
             .As<IUserRepository>()
             .InstancePerLifetimeScope();
        }
    }
}
