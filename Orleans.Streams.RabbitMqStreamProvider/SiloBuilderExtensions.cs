using System;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Streaming;
using Orleans.Streams.BatchContainer;

namespace Orleans.Hosting
{
    public static class SiloHostBuilderExtensions
    {
        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        [Obsolete("Use 'UseRabbitMqStream")]
        public static ISiloHostBuilder AddRabbitMqStream(this ISiloHostBuilder builder, string name, Action<SiloRabbitMqStreamConfigurator<DefaultBatchContainerSerializer>> configure = null)
        {
            return UseRabbitMqStreams(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        [Obsolete("Use 'UseRabbitMqStream")]
        public static ISiloHostBuilder AddRabbitMqStream<TSerializer>(this ISiloHostBuilder builder, string name, Action<SiloRabbitMqStreamConfigurator<TSerializer>> configure = null)
            where TSerializer : IBatchContainerSerializer, new()
        {
            return UseRabbitMqStreams(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static ISiloHostBuilder UseRabbitMqStreams(this ISiloHostBuilder builder, string name, Action<RabbitMqOptions> configureOptions)
        {
            return UseRabbitMqStreams<DefaultBatchContainerSerializer>(builder, name, configureOptions);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static ISiloHostBuilder UseRabbitMqStreams<TSerializer>(this ISiloHostBuilder builder, string name, Action<RabbitMqOptions> configureOptions)
            where TSerializer : IBatchContainerSerializer, new()
        {
            builder.UseRabbitMqStreams<TSerializer>(name, b => b.ConfigureRabbitMq(ob => ob.Configure(configureOptions)));

            return builder;
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static ISiloHostBuilder UseRabbitMqStreams(this ISiloHostBuilder builder, string name, Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
        {
            return UseRabbitMqStreams<DefaultBatchContainerSerializer>(builder, name, configureOptions);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static ISiloHostBuilder UseRabbitMqStreams<TSerializer>(this ISiloHostBuilder builder, string name, Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
            where TSerializer : IBatchContainerSerializer, new()
        {
            builder.UseRabbitMqStreams<TSerializer>(name, b => b.ConfigureRabbitMq(configureOptions));

            return builder;
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static ISiloHostBuilder UseRabbitMqStreams(this ISiloHostBuilder builder, string name, Action<SiloRabbitMqStreamConfigurator<DefaultBatchContainerSerializer>> configure = null)
        {
            return UseRabbitMqStreams<DefaultBatchContainerSerializer>(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static ISiloHostBuilder UseRabbitMqStreams<TSerializer>(this ISiloHostBuilder builder, string name, Action<SiloRabbitMqStreamConfigurator<TSerializer>> configure = null)
            where TSerializer : IBatchContainerSerializer, new()
        {
            var configurator = new SiloRabbitMqStreamConfigurator<TSerializer>(name,
                configureServicesDelegate => builder.ConfigureServices(configureServicesDelegate),
                configureAppPartsDelegate => builder.ConfigureApplicationParts(configureAppPartsDelegate)
            );

            configure?.Invoke(configurator);

            return builder;
        }
    }

    public static class SiloBuilderExtensions
    {
        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static ISiloBuilder UseRabbitMqStreams(this ISiloBuilder builder, string name, Action<RabbitMqOptions> configureOptions)
        {
            return UseRabbitMqStreams<DefaultBatchContainerSerializer>(builder, name, configureOptions);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static ISiloBuilder UseRabbitMqStreams<TSerializer>(this ISiloBuilder builder, string name, Action<RabbitMqOptions> configureOptions)
            where TSerializer : IBatchContainerSerializer, new()
        {
            builder.UseRabbitMqStreams<TSerializer>(name, b => b.ConfigureRabbitMq(ob => ob.Configure(configureOptions)));

            return builder;
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static ISiloBuilder UseRabbitMqStreams(this ISiloBuilder builder, string name, Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
        {
            return UseRabbitMqStreams<DefaultBatchContainerSerializer>(builder, name, configureOptions);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static ISiloBuilder UseRabbitMqStreams<TSerializer>(this ISiloBuilder builder, string name, Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
            where TSerializer : IBatchContainerSerializer, new()
        {
            builder.UseRabbitMqStreams<TSerializer>(name, b => b.ConfigureRabbitMq(configureOptions));

            return builder;
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static ISiloBuilder UseRabbitMqStreams(this ISiloBuilder builder, string name, Action<SiloRabbitMqStreamConfigurator<DefaultBatchContainerSerializer>> configure = null)
        {
            return UseRabbitMqStreams<DefaultBatchContainerSerializer>(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static ISiloBuilder UseRabbitMqStreams<TSerializer>(this ISiloBuilder builder, string name, Action<SiloRabbitMqStreamConfigurator<TSerializer>> configure = null)
            where TSerializer : IBatchContainerSerializer, new()
        {
            var configurator = new SiloRabbitMqStreamConfigurator<TSerializer>(name,
                configureServicesDelegate => builder.ConfigureServices(configureServicesDelegate),
                configureAppPartsDelegate => builder.ConfigureApplicationParts(configureAppPartsDelegate)
            );

            configure?.Invoke(configurator);

            return builder;
        }
    }
}