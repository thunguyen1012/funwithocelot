using Common;
using Common.Interfaces;
using Confluent.Kafka;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Order.Infrastructure.ServiceBus
{
    public class SubscriberBus : ISubscriber
    {
        public readonly string brokerList;
        public readonly string topic;
        private ClientConfig config;

        public SubscriberBus(string brokerList, string topic)
        {
            this.brokerList = brokerList;
            this.topic = "payment";
            config = new ClientConfig(new Dictionary<string, string>()
                {
                    {
                        "bootstrap.servers", "10.211.55.2:9092" //brokerList
                    },
                    { "group.id", "jambo-consumer" },
                    { "enable.auto.commit", "true" },
                    { "auto.commit.interval.ms", "5000" },
                    { "statistics.interval.ms", "60000" }
                });
        }

        public void Listen(IMediator mediator)
        {
            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            {
                consumer.Subscribe(topic);

                Console.WriteLine($"Subscribed to: [{string.Join(", ", consumer.Subscription)}]");

                var cancelled = false;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };

                Console.WriteLine("Ctrl-C to exit.");
                while (!cancelled)
                {
                    var @event = consumer.Consume(TimeSpan.FromSeconds(1));

                    if (@event != null)
                    {
                        try
                        {
                            //Type eventType = Type.GetType(@event.Message.Key);
                            //var domainEvent = (BaseDomainEvent)JsonConvert.DeserializeObject(@event.Message.Value, eventType);
                            //mediator.Send(domainEvent).Wait();


                            // TODO https://www.confluent.io/blog/put-several-event-types-kafka-topic/
                            var eventType = Type.GetType($"Order.Core.Events.{@event.Message.Key}");

                            // HACK for testing
                            if (@event.Message.Key.Equals("PaymentStatusUpdatedDomainEvent", StringComparison.CurrentCultureIgnoreCase))
                            {
                                eventType = typeof(Core.Events.PaymentStatusUpdatedDomainEvent);
                                var domainEvent = (BaseDomainEvent)JsonConvert.DeserializeObject(@event.Message.Value, eventType);
                                mediator.Send(domainEvent).Wait();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}