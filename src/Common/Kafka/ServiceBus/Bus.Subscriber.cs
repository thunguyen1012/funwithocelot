using Common.Interfaces;
using Confluent.Kafka;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Common.Kafka.ServiceBus
{
    public abstract class BaseSubscriberBus : ISubscriber
    {
        public string BrokerList { get; set; }
        public string Topic { get; set; }
        private ClientConfig config;

        public BaseSubscriberBus(string brokerList, string topic)
        {
            BrokerList = brokerList;
            Topic = topic;
            config = new ConsumerConfig
            {
                BootstrapServers = BrokerList,
                GroupId = "funwithocelot",
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 60000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };
        }

        public void Listen(IMediator mediator, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(config)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                })
                .Build();
            {
                consumer.Subscribe(Topic);

                Console.WriteLine($"Subscribed to: [{string.Join(", ", consumer.Subscription)}]");

                try
                {
                    while (true)
                    {
                        try
                        {
                            var @event = consumer.Consume(cancellationToken);

                            if (@event.IsPartitionEOF)
                            {
                                Console.WriteLine(
                                    $"Reached end of topic {@event.Topic}, partition {@event.Partition}, offset {@event.Offset}.");

                                continue;
                            }

                            Console.WriteLine($"Received message at {@event.TopicPartitionOffset}: {@event.Message.Value}");

                            try
                            {
                                var eventType = Type.GetType(@event.Message.Key);
                                if (eventType != null)
                                {
                                    var domainEvent = (BaseDomainEvent)JsonConvert.DeserializeObject(@event.Message.Value, eventType);
                                    mediator.Send(domainEvent).Wait();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Closing consumer.");
                    consumer.Close();
                }
            }
        }
    }
}