using Autofac;
using Common.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Order.Infrastructure.Data;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace Order.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private bool isDevelopment = false;
        private List<Assembly> assemblies = new List<Assembly>();

        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            this.isDevelopment = isDevelopment;

            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            assemblies.Add(infrastructureAssembly);

            if (callingAssembly != null)
            {
                assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EfRepository>().As<IRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Core.Services.OrderService>()
                .As<Core.Interfaces.IOrderService>()
                .InstancePerLifetimeScope();

            RegisterMediator(builder);
        }

        private void RegisterMediator(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }
    }
}