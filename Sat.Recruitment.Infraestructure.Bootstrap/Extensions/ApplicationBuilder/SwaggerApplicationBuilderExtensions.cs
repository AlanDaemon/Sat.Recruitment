using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ApplicationBuilder
{
    public static class SwaggerApplicationBuilderExtensions
    {
        public static void UseMicroserviceExampleSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {                
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"./swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    c.RoutePrefix = string.Empty;
                }

                c.SupportedSubmitMethods(SubmitMethod.Head, SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                c.EnableValidator();
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
