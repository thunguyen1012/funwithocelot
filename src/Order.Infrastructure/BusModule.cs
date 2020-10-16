using Autofac;
using Common.Interfaces;
using Order.Infrastructure.ServiceBus;

namespace Order.Infrastructure
{
    public class BusModule : Module
    {
        public string BrokerList { get; set; }
        public string ToTopic { get; set; }
        public string FromTopic { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublisherBus>()
                .As<IPublisher>()
                .WithParameter("brokerList", BrokerList)
                .WithParameter("topic", ToTopic)
                .SingleInstance();

            builder.RegisterType<SubscriberBus>()
                .As<ISubscriber>()
                .WithParameter("brokerList", BrokerList)
                .WithParameter("topic", FromTopic)
                .SingleInstance();
        }
    }
}