using Autofac;
using MediatR;
using Sat.Recruitment.Application.Behaviors;

namespace Sat.Recruitment.Infrastructure.Bootstrap.AutofacModules
{
    internal class MediatorModule : Module
    {
        private readonly bool enableCommandLogging;

        public MediatorModule(bool enableCommandLogging)
        {
            this.enableCommandLogging = enableCommandLogging;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //Discover entire "Application" service layer
            builder.RegisterAssemblyTypes(typeof(ValidatorBehavior<,>).Assembly)
                .AsImplementedInterfaces();

            if (this.enableCommandLogging)
            {
                builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            }

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}