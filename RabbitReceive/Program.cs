using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RabbitReceive
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "QueueRequestResource",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<ResponseResource>(json);

                Console.WriteLine($" [x] Received - ResourceName:{message.ResourceName}, Date: {message.Date}");
            };

            channel.BasicConsume(queue: "QueueRequestResource",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    public class ResponseResource
    {
        /// <summary>
        /// Sugestão de nome do recurso a ser criado no Portal do Azure.
        /// </summary>
        /// <example>MeuRecurso</example>
        [Required]
        public string ResourceName { get; set; }

        /// <summary>
        /// Tipo de recurso a ser criado.\
        /// 1 - Web App\
        /// 2 - SQL Database\
        /// 3 - Function App
        /// </summary>
        /// <example>1</example>
        [Required]
        public ResourceAzureType? ResourceAzureType { get; set; }

        /// <summary>
        /// Observações ou comentários sobre a solicitação.
        /// </summary>
        /// <example>Favor criar recurso em servidor localizado no Brasil.</example>
        public string Note { get; set; }

        /// <summary>
        /// Identificação do desenvolvedor.
        /// </summary>
        /// <example>gustavotrindade@dev.com</example>
        public string Email { get; set; }

        public DateTime Date { get; set; }
    }

    public enum ResourceAzureType
    {
        [Display(Name = "web_api")]
        WEB_APP = 1,

        [Display(Name = "sql_database")]
        SQL_DATABASE = 2,

        [Display(Name = "function_app")]
        FUNCTION_APP = 3
    }
}