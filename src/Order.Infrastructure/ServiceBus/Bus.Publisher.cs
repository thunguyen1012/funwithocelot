using Common.Kafka.ServiceBus;

namespace Order.Infrastructure.ServiceBus
{
    public class PublisherBus : BasePublisherBus
    {
        public PublisherBus(string brokerList, string topic)
            : base(brokerList, topic)
        {
        }
    }
}