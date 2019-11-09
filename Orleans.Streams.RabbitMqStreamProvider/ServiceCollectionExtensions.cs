using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Configuration;

namespace Orleans.Hosting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMqStreams(this IServiceCollection services, string name, IConfiguration configuration)
        {
            return AddRabbitMqStreams(services, name, configureOptions => configureOptions.Bind(configuration));
        }

        public static IServiceCollection AddRabbitMqStreams(this IServiceCollection services, string name, Action<RabbitMqOptions> options)
        {
            return AddRabbitMqStreams(services, name, configureOptions => configureOptions.Configure(options));
        }

        public static IServiceCollection AddRabbitMqStreams(this IServiceCollection services, string name, Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
        {
            configureOptions(services.AddOptions<RabbitMqOptions>(name));

            return services;
        }
    }
}