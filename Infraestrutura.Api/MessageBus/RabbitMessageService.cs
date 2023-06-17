using Infraestrutura.Api.Models.Request;
using Infraestrutura.Api.RabbitServices.Constants;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infraestrutura.Api.RabbitServices
{
    public class RabbitMessageService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private const string TrackingsExchange = "trackings-service";
        private const string RoutingKeySubscribe = "shipping-order-updated";

        public RabbitMessageService(IServiceProvider serviceProvider)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("shipping-order-updated-consumer");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(TrackingsExchange, "topic", true, false);

            _channel.QueueDeclare(
                queue: Queues.QUEUE_REQUEST_RESOURCE,
                durable: false,
                exclusive: false,
                autoDelete: false);

            _channel.QueueBind(Queues.QUEUE_REQUEST_RESOURCE, TrackingsExchange, RoutingKeySubscribe);

            _serviceProvider = serviceProvider;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<RequestResource>(json);

                //Notify(@event).Wait();

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: Queues.QUEUE_REQUEST_RESOURCE,
                                 autoAck: false,
                                 consumer: consumer);
            

            return Task.CompletedTask;
        }
    }
}
