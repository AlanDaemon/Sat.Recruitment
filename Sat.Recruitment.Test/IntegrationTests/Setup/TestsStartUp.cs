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
using Sat.Recruitment.Api;
using Sat.Recruitment.Infraestructure.Database;
using Sat.Recruitment.Infrastructure.Bootstrap;
using Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ServiceCollection;
using System;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Test.IntegrationTests.Setup
{
    public class TestsStartUp : Startup
    {
        readonly IConfiguration _configuration;
       
        public TestsStartUp(IConfiguration configuration) : base(configuration)
        {
            this._configuration = configuration;
        }

        protected override void ConfigureServicesAux(IServiceCollection services)
        {
            services.AddCorsConfiguration();
            services.AddHealthChecks();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc()
            .AddApplicationPart(typeof(Startup).Assembly)
            .AddMvcOptions(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                                            new SlugifyParameterTransformer()));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddSwagger();
            services.ConfigureResponseCompression();
            services.AddHttpContextAccessor();
            services.AddEntityFrameworkSqlServer().AddDbContext<RecruitmentDbContext>();
            services.AddMediatR(typeof(Application.Behaviors.ValidatorBehavior<,>).Assembly);
            services.AddApiVersion("1.0");
            services.AddControllers(o =>
            {
                o.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                o.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
           });
        }       
    }
}
