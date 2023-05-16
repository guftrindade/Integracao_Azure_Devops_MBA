﻿using Devops.RabbitServices.Constants;
using Devops.RabbitServices.Interfaces;
using Devops.ViewModels.Infrastructure.Request;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Devops.RabbitServices
{
    public class RabbitMessageService : IRabbitMessageService
    {
        public void Send(RequestResource message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Queues.REQUEST_RESOURCE,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: Queues.REQUEST_RESOURCE,
                                 basicProperties: null,
                                 body: body);
        }
        
    }
}
