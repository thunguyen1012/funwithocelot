using Common.Interfaces;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Kafka.ServiceBus
{
    public abstract class BasePublisherBus : IPublisher
    {
        public string BrokerList { get; set; }
        public string Topic { get; set; }

        private ClientConfig config;

        public BasePublisherBus(string brokerList, string topic)
        {
            BrokerList = brokerList;
            Topic = topic;
            config = new ClientConfig(new Dictionary<string, string>()
                {
                    {
                        "bootstrap.servers", BrokerList
                    }
                });
        }

        public async Task Publish(BaseDomainEvent domainEvent)
        {
            var val = JsonConvert.SerializeObject(domainEvent, Formatting.Indented);
            var key = domainEvent.GetType().AssemblyQualifiedName;

            using var producer = new ProducerBuilder<string, string>(config).Build();
            await producer.ProduceAsync(Topic, new Message<string, string> { Key = key, Value = val });
        }

        public async Task Publish(IEnumerable<BaseDomainEvent> domainEvents, Interfaces.IHeader header)
        {
            foreach (var domainEvent in domainEvents)
            {
                domainEvent.SetHeader(header);
                await Publish(domainEvent);
            }
        }
    }
}