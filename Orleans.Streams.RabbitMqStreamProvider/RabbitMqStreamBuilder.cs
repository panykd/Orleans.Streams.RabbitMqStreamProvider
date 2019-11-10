using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers.Streams.Common;
using Orleans.Streams;
using Orleans.Streams.BatchContainer;

namespace Orleans.Streaming
{

    public class SiloRabbitMqStreamConfigurator<TSerializer> : SiloPersistentStreamConfigurator
        where TSerializer : IBatchContainerSerializer, new()
    {
        [Obsolete]
        public SiloRabbitMqStreamConfigurator(string name, Action<Action<IServiceCollection>> configureDelegate)
            : this(name, configureDelegate, configureAppPartsDelegate => { })
        {

        }

        public SiloRabbitMqStreamConfigurator(string name, Action<Action<IServiceCollection>> configureServicesDelegate, Action<Action<IApplicationPartManager>> configureAppPartsDelegate)
            : base(name, configureServicesDelegate, RabbitMqAdapterFactory<TSerializer>.Create)
        {

            configureAppPartsDelegate(parts =>
            {
                parts
                    .AddFrameworkPart(typeof(RabbitMqAdapter).Assembly)
                    .AddFrameworkPart(typeof(EventSequenceTokenV2).Assembly);
            });

            ConfigureDelegate(services =>
            {
                services
                    .ConfigureNamedOptionForLogging<RabbitMqOptions>(name)
                    .ConfigureNamedOptionForLogging<CachingOptions>(name)
                    .AddTransient<IConfigurationValidator>(sp => new RabbitMqOptionsValidator(sp.GetOptionsByName<RabbitMqOptions>(name), name))
                    .AddTransient<IConfigurationValidator>(sp => new CachingOptionsValidator(sp.GetOptionsByName<CachingOptions>(name), name));
            });
        }

        public SiloRabbitMqStreamConfigurator<TSerializer> ConfigureRabbitMq(
            string host, int port, string virtualHost, string user, string password, string queueName,
            bool useQueuePartitioning = RabbitMqOptions.DefaultUseQueuePartitioning,
            int numberOfQueues = RabbitMqOptions.DefaultNumberOfQueues)
        {
            return ConfigureRabbitMq(configureOptions => configureOptions.Configure(options =>
            {
                options.HostName = host;
                options.Port = port;
                options.VirtualHost = virtualHost;
                options.UserName = user;
                options.Password = password;
                options.QueueNamePrefix = queueName;
                options.UseQueuePartitioning = useQueuePartitioning;
                options.NumberOfQueues = numberOfQueues;
            }));
        }

        public SiloRabbitMqStreamConfigurator<TSerializer> ConfigureRabbitMq(Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
        {
            this.Configure(configureOptions);

            return this;
        }

        public SiloRabbitMqStreamConfigurator<TSerializer> ConfigureCache(Action<OptionsBuilder<CachingOptions>> configureOptions)
        {
            this.Configure(configureOptions);

            return this;
        }
    }

    public class ClusterClientRabbitMqStreamConfigurator<TSerializer> : ClusterClientPersistentStreamConfigurator
        where TSerializer : IBatchContainerSerializer, new()
    {
        public ClusterClientRabbitMqStreamConfigurator(string name, IClientBuilder clientBuilder)
            : base(name, clientBuilder, RabbitMqAdapterFactory<TSerializer>.Create)
        {
            clientBuilder
                .ConfigureApplicationParts(parts =>
                {
                    parts
                        .AddFrameworkPart(typeof(RabbitMqAdapterFactory<TSerializer>).Assembly)
                        .AddApplicationPart(typeof(EventSequenceTokenV2).Assembly);
                })
                .ConfigureServices(services =>
                {
                    services
                        .ConfigureNamedOptionForLogging<RabbitMqOptions>(name)
                        .AddTransient<IConfigurationValidator>(sp => new RabbitMqOptionsValidator(sp.GetOptionsByName<RabbitMqOptions>(name), name));
                });
        }

        public ClusterClientRabbitMqStreamConfigurator<TSerializer> ConfigureRabbitMq(Action<OptionsBuilder<RabbitMqOptions>> configureOptions)
        {
            this.Configure(configureOptions);
            return this;
        }
    }
}