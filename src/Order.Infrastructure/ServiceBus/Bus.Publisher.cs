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
        public string BrokerList { get; set; }
        public string Topic { get; set; }

        private ClientConfig config;

        public PublisherBus()
        {
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