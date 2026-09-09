using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shop.Application.DTOs.OrderDTOs;
using Shop.Application.Interfaces.Services;
using Shop.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.Api.Services;


    /// <summary>
    /// Робота класа запускається у фоновому режимі, всі логі пишутся в 1 консоль
    /// </summary>
    public class RabbitMqOrderReaderService : BackgroundService
    {
        private readonly ILogger<RabbitMqOrderReaderService> _logger;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public RabbitMqOrderReaderService(
            ILogger<RabbitMqOrderReaderService> logger,
            IOptions<RabbitMqSettings> options,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _rabbitMqSettings = options.Value;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.Host,
                Port = _rabbitMqSettings.Port
            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, e) =>
            {
                var body = e.Body.ToArray();

                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<OrderResponseDTO>(json);

                if (message == null)
                    return;

                //_logger.LogInformation(
                //    "Email: {Email}",
                //    message.Email);

                //_logger.LogInformation(
                //    "Id: {Id}",
                //    message.Id);
                using var scope = _scopeFactory.CreateScope();

                var orderProcessingService =
                    scope.ServiceProvider.GetRequiredService<IOrderProcessingService>();

                await orderProcessingService.ProcessAsync(message, stoppingToken);

            };

            await _channel.BasicConsumeAsync(
                queue: "Orders",
                autoAck: true,
                consumer: consumer);

            _logger.LogInformation(
                "RabbitMQ Reader started. Waiting messages...");

            // Замість Console.ReadLine()
            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "RabbitMQ Reader stopping...");

            if (_channel != null)
                await _channel.CloseAsync();

            if (_connection != null)
                await _connection.CloseAsync();

            await base.StopAsync(cancellationToken);
        }
    }

