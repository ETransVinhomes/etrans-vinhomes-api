using System.Text;
using Auth.Services.EventProcessing.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Auth.Services.AsyncDataServices;
public class MessageBusSuscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection = default!;
    private IModel _channel = default!;
    private string _queueName = default!;
    public MessageBusSuscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
        InitializeRabbitMQ();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ModuleHandle, ea) =>
        {
            System.Console.WriteLine("--> Event Received");
            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
            _eventProcessor.ProcessEvent(notificationMessage);
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]!)
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName,
        exchange: "trigger", routingKey: "");
        System.Console.WriteLine("--> Listening on the Message Bus...");
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown!;

    }
    private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
    {
        System.Console.WriteLine("--> Connection Shut Down");
    }
    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        base.Dispose();
    }
}
