using Microsoft.AspNetCore.Builder;

namespace Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ApplicationBuilder
{
    public static class CorsApplicationBuilderExtensions
    {
        public static void UseCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }
    }
}
