using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuProducerService.Infrastructure.MessageBroker
{
    public interface IRabbitMQProducer
    {
        Task PublishAsync(string queueName, object message);
    }
}
