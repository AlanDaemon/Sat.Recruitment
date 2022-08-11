using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class CompressionServiceCollectionExtensions
    {
        public static void ConfigureResponseCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
        }
    }
}
