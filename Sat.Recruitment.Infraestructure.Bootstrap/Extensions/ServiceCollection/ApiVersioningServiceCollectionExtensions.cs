﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Sat.Recruitment.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class ApiVersioningServiceCollectionExtensions
    {
        public static void AddApiVersion(this IServiceCollection services, string defaultVersion)
        {

            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;                
                o.DefaultApiVersion = ApiVersion.Parse(defaultVersion);
            });
        }
    }
}
