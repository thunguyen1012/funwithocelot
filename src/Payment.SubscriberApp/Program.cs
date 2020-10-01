using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Infrastructure;
using System;

namespace Payment.SubscriberApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("autofac.json")
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            IServiceCollection serviceCollection = new ServiceCollection();

            Startup startup = new Startup(configuration);
            startup.ConfigureServices(serviceCollection);
            startup.Run();
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IServiceProvider serviceProvider;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ContainerBuilder builder = new ContainerBuilder();

            string connectionString = Configuration.GetConnectionString("SqliteConnection");
            services.AddDbContext(connectionString);

            builder.Populate(services);
            builder.RegisterModule(new ConfigurationModule(Configuration));

            serviceProvider = new AutofacServiceProvider(builder.Build());

            return serviceProvider;
        }

        public void Run()
        {
            IMediator mediator = serviceProvider.GetService<IMediator>();
            ISubscriber subscriber = serviceProvider.GetService<ISubscriber>();

            subscriber.Listen(mediator);
        }
    }
}