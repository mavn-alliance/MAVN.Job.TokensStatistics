using JetBrains.Annotations;
using Lykke.Job.TokensStatistics.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using Lykke.Job.TokensStatistics.MappingProfiles;
using Lykke.Sdk;

namespace Lykke.Job.TokensStatistics
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "TokensStatisticsJob API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "TokensStatisticsJobLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.TokensStatisticsJob.Db.LogsConnString;
                };

                options.Extend = (serviceCollection, settings) =>
                {
                    serviceCollection.AddAutoMapper(typeof(AutoMapperProfile));
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IConfigurationProvider mapper)
        {
            mapper.AssertConfigurationIsValid();
            
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;
            });
        }
    }
}
