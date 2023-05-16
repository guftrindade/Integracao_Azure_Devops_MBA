using Infraestrutura.Api.Models.Request;
using Infraestrutura.Api.Models.Response;
using Infraestrutura.Api.RabbitServices.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infraestrutura.Api.RabbitServices
{
    public class RabbitMessageService : IRabbitMessageService
    {
        public void Receive(RequestResource message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "RequestResource",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var mensagem = JsonSerializer.Deserialize<ResponseResource>(json);
            };

            channel.BasicConsume(queue: "RequestResource",
                                 autoAck: true,
                                 consumer: consumer);
           
        }
    }
}
