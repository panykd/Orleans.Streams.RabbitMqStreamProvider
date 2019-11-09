using System;
using Orleans.Configuration;
using Orleans.Streaming;
using Orleans.Streams.BatchContainer;

namespace Orleans.Hosting
{
    public static class ClientBuilderExtensions
    {
        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        [Obsolete("Use 'UseRabbitMqSteams'")]
        public static IClientBuilder AddRabbitMqStream(this IClientBuilder builder, string name, Action<ClusterClientRabbitMqStreamConfigurator<DefaultBatchContainerSerializer>> configure = null)
        {
            return UseRabbitMqStreams(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        [Obsolete("Use 'UseRabbitMqSteams'")]
        public static IClientBuilder AddRabbitMqStream<TSerializer>(this IClientBuilder builder, string name, Action<ClusterClientRabbitMqStreamConfigurator<TSerializer>> configure = null)
            where TSerializer : IBatchContainerSerializer, new()
        {
            return UseRabbitMqStreams(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static IClientBuilder UseRabbitMqStreams(this IClientBuilder builder, string name, Action<RabbitMqOptions> options)
        {
            return UseRabbitMqStream<DefaultBatchContainerSerializer>(builder, name, options);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static IClientBuilder UseRabbitMqStreams<TSerializer>(this IClientBuilder builder, string name, Action<RabbitMqOptions> options)
            where TSerializer : IBatchContainerSerializer, new()
        {
            return builder.UseRabbitMqStream<TSerializer>(name, b => b.ConfigureRabbitMq(ob => ob.Configure(options)));

        }

        /// <summary>
        /// Configure client to use RMQ persistent streams, using the <see cref="DefaultBatchContainerSerializer"/>.
        /// </summary>
        public static IClientBuilder UseRabbitMqStreams(this IClientBuilder builder, string name, Action<ClusterClientRabbitMqStreamConfigurator<DefaultBatchContainerSerializer>> configure = null)
        {
            return UseRabbitMqStream<DefaultBatchContainerSerializer>(builder, name, configure);
        }

        /// <summary>
        /// Configure client to use RMQ persistent streams.
        /// </summary>
        public static IClientBuilder UseRabbitMqStreams<TSerializer>(this IClientBuilder builder, string name, Action<ClusterClientRabbitMqStreamConfigurator<TSerializer>> configure = null)
            where TSerializer : IBatchContainerSerializer, new()
        {
            var configurator = new ClusterClientRabbitMqStreamConfigurator<TSerializer>(name, builder);
            configure?.Invoke(configurator);

            return builder;
        }
    }
}