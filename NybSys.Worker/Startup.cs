using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NybSys.Common.Utility;
using System;
using System.IO;
using NybSys.MassTransit;
using NybSys.Mqtt;
using NybSys.Worker.Request_Handler;

namespace NybSys.Worker
{
    class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /// Dependency Injection Implementation Here Or Inject Servie Collection
            /// services.AddTransit<>();
            /// Here Input the BaseUri of HttpRequest


            AddMassTransit(services);

            AddMqtt(services);


            // This is service provider for create instance of DI object
            ServiceProvider provider = services.BuildServiceProvider();


            // Now you will able to execute your desired process

        }

        private IServiceCollection AddMassTransit(IServiceCollection services)
        {
            services.AddAspNetCoreMassTransit(config =>
            {
                config.SetupConnection(connection =>
                {
                    connection.RabbitMQConnectionString = Configuration["MassTransit:Host"];
                    connection.Port = Configuration["MassTransit:Port"];
                    connection.Timeout = Convert.ToInt32(Configuration["MassTransit:Timeout"]);
                    connection.Username = Configuration["MassTransit:Username"];
                    connection.Password = Configuration["MassTransit:Password"];
                }, "Worker");

                config.AddCosumers(consumer =>
                {
                    consumer.AddConsumer<RequestConsumer>();
                });
            });

            return services;
        }

        private IServiceCollection AddMqtt(IServiceCollection services)
        {
            services.AddMqttServer(config =>
            {
                config.AddConnection(connection =>
                {
                    connection.MQBrokerHost = Configuration["Mqtt:Host"];
                    connection.MQbrokerPort = Convert.ToInt32(Configuration["Mqtt:Port"]);
                    connection.AlivePeriod = Convert.ToInt32(Configuration["Mqtt:AlivePeriod"]);
                    connection.ConnectionTimeout = Convert.ToInt32(Configuration["Mqtt:ConnectionTimeout"]);
                });

                config.AddConsumer<MqttConsumer>();
            });

            return services;
        }
    }
}
