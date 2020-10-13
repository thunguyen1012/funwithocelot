using Common;
using Common.Interfaces;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Infrastructure.ServiceBus
{
    public class PublisherBus : IPublisher
    {
        public readonly string brokerList;
        public readonly string topic;
        private ClientConfig config;

        public PublisherBus(string brokerList, string topic)
        {
            this.brokerList = brokerList;
            this.topic = "order";
            config = new ClientConfig(new Dictionary<string, string>()
                {
                    {
                        "bootstrap.servers", "10.211.55.2:9092" //brokerList
                    }
                });
        }

        public async Task Publish(BaseDomainEvent domainEvent)
        {
            var val = JsonConvert.SerializeObject(domainEvent, Formatting.Indented);
            var key = domainEvent.GetType().AssemblyQualifiedName;

            using var producer = new ProducerBuilder<string, string>(config).Build();
            await producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = val });
        }

        public async Task Publish(IEnumerable<BaseDomainEvent> domainEvents, Common.Interfaces.IHeader header)
        {
            foreach (var domainEvent in domainEvents)
            {
                domainEvent.SetHeader(header);
                await Publish(domainEvent);
            }
        }
    }
}