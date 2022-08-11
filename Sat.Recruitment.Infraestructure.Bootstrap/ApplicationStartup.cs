using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Infraestructure.Database;
using Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ApplicationBuilder;
using Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ServiceCollection;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Infrastructure.Bootstrap
{
    public class ApplicationStartup
    {
        private readonly IConfiguration configuration;

        public ApplicationStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsConfiguration();
            services.AddHealthChecks();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc().AddMvcOptions(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                                            new SlugifyParameterTransformer()));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddEntityFrameworkSqlServer().AddDbContext<RecruitmentDbContext>();
            services.AddSwagger();
            services.ConfigureResponseCompression();
            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(Application.Behaviors.ValidatorBehavior<,>).Assembly);
            services.AddApiVersion(this.configuration.GetValue<string>("AppSettings:DefaultApiVersion", "1.0"));
            services.AddControllers(o =>
            {
                o.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                o.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            });   
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddConfigurationAutofac(this.configuration);
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == Environments.Development)
            {
                // Creates Database if not exists. This is only for testing purposes.
                var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
                using var serviceScope = serviceScopeFactory.CreateScope();
                var dbContext = serviceScope.ServiceProvider.GetService<RecruitmentDbContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseCorsConfiguration();
            app.UseRouting();
            app.UseMicroserviceExampleHealthChecks();
            app.UseResponseCompression();
            app.UseMicroserviceExampleSwagger(provider);
            app.UseExceptionHandler(errorPipeline =>
            {
                errorPipeline.UseExceptionHandlerMiddleware(this.configuration.GetValue("AppSettings:IncludeErrorDetailInResponse", false));
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }       
    }
}
