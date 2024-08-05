using Confluent.Kafka;
using System;
using System.Threading.Tasks;
namespace PermissionsWeb.Application.Services
{


    public class KafkaProducerService
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducerService(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            try
            {
                var result = await _producer.ProduceAsync(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
                Console.WriteLine($"Message '{result.Value}' sent to partition {result.Partition} with offset {result.Offset}");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Message} [{e.Error.Code}]");
            }
        }
    }
}

