using Common.Kafka.ServiceBus;

namespace Payment.Infrastructure.ServiceBus
{
    public class SubscriberBus : BaseSubscriberBus
    {
        public SubscriberBus(string brokerList, string topic)
            : base(brokerList, topic)
        {
        }
    }
}